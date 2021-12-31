using ShadowClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Timers;

namespace Shadow_RDP
{
    public partial class frmShadowRdp : Form
    {
        #region FormVars
        private bool FirstItemAdded = false;
        private bool loadint = false;
        private bool closing = false;
        //private frmSplash loading = null;
        
        private bool statusRefreshRunning = false;
        private bool vdiRefreshRunning = false;

        private List<string[]> RefreshList = new List<string[]>();

        private event OnRefreshComplete RefreshComplete;

        private delegate void OnRefreshComplete();
        private System.Timers.Timer statusRefresh = new System.Timers.Timer() { Interval = 1000, AutoReset = false };
        private System.Timers.Timer tmrRefresh = new System.Timers.Timer() { Interval = 1000, AutoReset = false };

        FormWindowState LastWindowState = FormWindowState.Minimized;
        ProxAPI prox = new ProxAPI();

        #endregion
        public frmShadowRdp()
        {
            InitializeComponent();

            ListViewColumnSorter defaultCS = new ListViewColumnSorter { Order = SortOrder.Ascending };
            lstRdps.ListViewItemSorter = Helper.CreateDeepCopy(defaultCS);
            lvQueryList.ListViewItemSorter = Helper.CreateDeepCopy(defaultCS);
            lvVdiStatus.ListViewItemSorter = Helper.CreateDeepCopy(defaultCS);
        }
        private void frmShadowRdp_Load(object sender, EventArgs e)
        {
            frmSplash frm = ShowSplash(tabShadowControl.SelectedTab);
            RefreshComplete += OnProcessCompleted;

            tmrRefresh.Elapsed += TmrRefresh_Elapsed;
            statusRefresh.Elapsed += StatusRefresh_Elapsed;
            Thread t = new Thread((form) => { GetActivityList(form); }) { IsBackground=true};
            t.Start(frm);
            
            t = new Thread(() => { populateVdiStatus(); }) { IsBackground = true };
            t.Start();
            t = null;
        }

        private void StatusRefresh_Elapsed(object sender, ElapsedEventArgs e)
        {
            populateVdiStatus();
        }

        private void TmrRefresh_Elapsed(object sender, ElapsedEventArgs e)
        {
            GetActivityList();
        }
        #region Methods

        private void LaunchRDP(ListViewItem itm)
        {
            PopRDP.Run(itm.SubItems[3].Text, itm.SubItems[0].Text);
        }
        private void LogoffUser(ListViewItem itm)
        {
            LogOffUser.LogOff(itm);
        }
        private void Restart(ListViewItem itm)
        {
            RestartPC.Restart(itm);
        }

        private void ResizeListViewColumns(ListView lv)
        {
            Thread.Sleep(10);
            Application.DoEvents();

            foreach (ColumnHeader column in lv.Columns)
            {
                column.Width = -2;
            }
        }
        private void GetActivityList(object form = null)
        {
            tmrRefresh.Stop();
            vdiRefreshRunning = true;
            if (closing) { vdiRefreshRunning = false; return; }
            if (!prox.ValidTicket)
            {
                prox = new ProxAPI();
            }
            
            List<Thread> threads = new List<Thread>();
            List<string> activeVMs = prox.GetAActiveVms;
            foreach (string s in activeVMs)
            {
                Thread t = new Thread((index) =>
                {
                    string cstr = "VDI" + index;
                    Query q = new Query(cstr);

                    if (q.ListUsers.Count <= 0) return;
                    foreach (string[] item in q.ListUsers)
                    {
                        
                        if (item.Length == 7 &&
                            (item[4].ToUpper() == "ACTIVE" || !cbHide.Checked))
                        {
                            RefreshList.Add(item);
                        }
                    }
                })
                { IsBackground = true };
                threads.Add(t);
                t.Start(s);
                t = null;

            }
            activeVMs.Clear();
            activeVMs = null;

            //wait for all threads to complete

            while ((from Thread t in threads where t.IsAlive select t).Count() > 0)
            {
                Thread.Sleep(10);
            }

            threads.Clear();
            threads = null;

            
            RefreshComplete();
            if (!FirstItemAdded)
            {
                lstRdps.Invoke((MethodInvoker)delegate { ResizeListViewColumns(lstRdps); });
                FirstItemAdded = true;
                if (!loadint)
                {
                    loadint = true;
                    frmSplash loadform = (frmSplash)form;
                    loadform.Invoke((MethodInvoker)delegate { loadform.Close(); });
                    loadform = null;
                }
            }
            //if (form != null)
            //{
            //    frmSplash loadform = (frmSplash)form;
            //    if (loadform.InvokeRequired)
            //    {
            //        Invoke((MethodInvoker)delegate { loadform.Close(); });
            //    }
            //    else
            //    {
            //        loadform.Close();
            //    }
            //    form = null;
            //    loading = null;
            //}

            Thread.Sleep(10);
        }
        protected virtual void OnProcessCompleted()
        {
            List<ListViewItem> curItems = new List<ListViewItem>();

            Invoke((MethodInvoker)delegate
            {
                foreach (ListViewItem l in lstRdps.Items)
                {
                    curItems.Add(l);
                }
            });

            foreach (ListViewItem tm in curItems)
            {

                string[] rltm = (from string[] l in RefreshList
                                     where l != null && l[0].ToUpper() == tm.SubItems[0].Text.ToUpper()
                                     select l).FirstOrDefault();

                if (rltm == null)
                {
                    //Remove Items not on RefreshList
                    Invoke((MethodInvoker)delegate { lstRdps.Items.Remove(tm); });
                }
                rltm = null;
            }
            curItems.Clear();
            curItems = null;

            foreach (string[] Item in RefreshList)
            {
                ListViewItem tm = null;
                Invoke((MethodInvoker)delegate
                            {
                                tm = (from ListViewItem l in lstRdps.Items
                                      where l != null && l.SubItems[0].Text.ToUpper() == Item[0].ToUpper()
                                      select l).FirstOrDefault();
                            });

                if (Item == null) continue;
                if (tm == null)
                {
                    //Add Missing item
                    lstRdps.Invoke((MethodInvoker)delegate { lstRdps.Items.Add(new ListViewItem(Item)); });
                }
                else
                {
                    List<int> updateindex = new List<int>();
                    foreach (string itm in Item)
                    {
                        if (tm.SubItems[Array.IndexOf(Item,itm)].Text != itm)
                        {
                            updateindex.Add(Array.IndexOf(Item, itm));
                        }
                    }
                    Invoke((MethodInvoker)delegate
                    {
                        
                        foreach (int i in updateindex)
                        {
                            //Update SubItmes
                            tm.SubItems[i].Text = Item[i];
                        }
                        
                    });
                    updateindex.Clear();
                    updateindex = null;
                }
                tm = null;
            }
            RefreshList.Clear();
            tmrRefresh.Start();
            vdiRefreshRunning = false;
        }

        private static frmSplash ShowSplash(Control parent, string displayText = "Loading...")
        {
            frmSplash frm = new frmSplash
            {
                DisplayText = displayText,
                TopLevel = false,
                Parent = parent
            };
            frm.BringToFront();

            frm.Left = (parent.Width / 2) - (frm.Width / 2);
            frm.Top = (parent.Height / 2) - (frm.Height / 2);

            frm.Show();
            return frm;
        }
        private void colSort(object sender, ColumnClickEventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewColumnSorter lvwColumnSorter = (ListViewColumnSorter)listView.ListViewItemSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                lvwColumnSorter.Order = lvwColumnSorter.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }


            // Perform the sort with these new sort options.
            listView.ListViewItemSorter = lvwColumnSorter;
            listView.Sort();
        }
        #endregion
        #region Events
        private void lstRdps_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem tm = lstRdps.SelectedItems[0];
            Thread t = new Thread((lvi) =>
            {
                ListViewItem itm = (ListViewItem)lvi;
                if (itm.SubItems[4].Text.Contains("Active"))
                {
                    LaunchRDP(itm);
                }
                else
                {
                    DialogResult Res = MessageBox.Show("Would you like to Restart Computer?", "Conformation",
                        MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        Restart(itm);
                    }
                }
            });
            t.IsBackground = true;
            t.Start(tm);
            t = null;
        }
        private void frmShadowRdp_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmSplash frm = ShowSplash(this, "Closing...");
            bool closefrm = false;
            tmrRefresh.Stop();

            statusRefresh.Stop();
            closing = true;
            Thread t = new Thread(() =>
            {
                while (!closefrm)
                {
                    switch (frm.DisplayText.Length)
                    {
                        case 7:
                            Invoke((MethodInvoker)delegate { frm.DisplayText = "Closing."; });
                            break;
                        case 8:
                            Invoke((MethodInvoker)delegate { frm.DisplayText = "Closing.."; });
                            break;
                        case 9:
                            Invoke((MethodInvoker)delegate { frm.DisplayText = "Closing..."; });
                            break;
                        case 10:
                            Invoke((MethodInvoker)delegate { frm.DisplayText = "Closing"; });
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(1000);
                }

            })
            { IsBackground = true };
            t.Start();
            while (vdiRefreshRunning || statusRefreshRunning)
            {                                
                Thread.Sleep(10);
                Application.DoEvents();
            }
            closefrm = true;
            frm.Close();
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            Thread t = new Thread(() =>
            {
                txtWorkStation.Text = txtWorkStation.Text.Trim();
                string message = "";
                if (!string.IsNullOrEmpty(txtWorkStation.Text) && ActiveDirectory.ComputerExists(txtWorkStation.Text))
                {
                    bool AllowShadow = false;
                    if (ActiveDirectory.IsRestricted(txtWorkStation.Text))
                    {
                        PasswordDialog pass = new PasswordDialog();
                        if (pass.ShowDialog() == DialogResult.OK)
                        {
                            AllowShadow = true;
                        }
                    }
                    else
                    {
                        AllowShadow = true;
                    }

                    if (AllowShadow)
                    {
                        Query q = new Query(txtWorkStation.Text);
                        if (q.ListUsers.Count > 0)
                        {
                            PopRDP.Run(q.ListUsers[0][3], txtWorkStation.Text);
                        }
                        else message = "No Users Found for this Workstation.";


                    }
                }
                else message = "Workstation Not Found.";

                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message);
                }
            });
            t.IsBackground = true;
            t.Start();
            connectButton.Enabled = true;
        }
        private void txtWorkStation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                connectButton_Click(this, new EventArgs());
            }
        }
        private void btnAddtoList_Click(object sender, EventArgs e)
        {
            btnAddtoList.Enabled = false;
            frmSplash frm;
            frm = ShowSplash(tabShadowControl.SelectedTab);

            Thread t = new Thread((form) =>
            {
                txtQueryBox.Text = txtQueryBox.Text.Trim();
                string message = "";
                if (!string.IsNullOrEmpty(txtQueryBox.Text) && ActiveDirectory.ComputerExists(txtQueryBox.Text))
                {
                    bool AllowShadow = false;
                    if (ActiveDirectory.IsRestricted(txtQueryBox.Text))
                    {
                        PasswordDialog pass = new PasswordDialog();
                        if (pass.ShowDialog() == DialogResult.OK)
                        {
                            AllowShadow = true;
                        }

                        pass = null;
                    }
                    else
                    {
                        AllowShadow = true;
                    }

                    if (AllowShadow)
                    {
                        Query q = new Query(txtQueryBox.Text);
                        if (q.ListUsers.Count > 0)
                        {
                            lvQueryList.Invoke((MethodInvoker)delegate
                            {
                                foreach (string[] itm in q.ListUsers)
                                {
                                    ListViewItem tm = new ListViewItem(itm);
                                    lvQueryList.Items.Add(tm);
                                }
                            });
                        }
                        else message = "No Users Found for this Workstation.";

                    }
                }
                else message = "Workstation Not Found.";

                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message);
                }
                frmSplash loadfrm = (frmSplash)form;
                Invoke((MethodInvoker)delegate { loadfrm.Close(); });
            });
            t.IsBackground = true;
            t.Start(frm);
            btnAddtoList.Enabled = true;
        }
        private void txtQueryBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddtoList_Click(this, new EventArgs());
            }
        }
        private void lvQueryList_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem lvi = lvQueryList.SelectedItems[0];
            Thread t = new Thread((listviewitem) =>
            {
                ListViewItem tm = (ListViewItem)listviewitem;
                if (tm.SubItems[4].Text.Contains("Active"))
                {
                    LaunchRDP(tm);
                }
                else
                {
                    DialogResult Res = MessageBox.Show("Would you like to Restart " + tm.SubItems[0].Text + "?", "Conformation",
                        MessageBoxButtons.YesNo);
                    if (Res == DialogResult.Yes)
                    {
                        Restart(tm);
                    }
                }
            });
            t.IsBackground = true;
            t.Start(lvi);
        }
        private void btnClearList_Click(object sender, EventArgs e)
        {
            lvQueryList.Items.Clear();
        }
        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            lstRdps.Items.Clear();
            //loading = ShowSplash(tabShadowControl.SelectedTab, "Refreshing");
            
        }
        #endregion

        private void tabShadowControl_Selected(object sender, TabControlEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                switch (e.TabPageIndex)
                {
                    case 0:
                        Invoke((MethodInvoker)delegate { ResizeListViewColumns(lstRdps); });
                        break;
                    case 1:
                        Invoke((MethodInvoker)delegate { ResizeListViewColumns(lvQueryList); });
                        break;
                    default:
                        Invoke((MethodInvoker)delegate { ResizeListViewColumns(lvVdiStatus); });
                        break;
                }
            })
            {
                IsBackground = true
            };
            t.Start();



        }

        private void frmShadowRdp_ResizeEnd(object sender, EventArgs e)
        {
            int i = tabShadowControl.SelectedIndex;
            Thread t = new Thread((index) =>
            {
                if ((int)index == 0)
                {
                    Invoke((MethodInvoker)delegate { ResizeListViewColumns(lstRdps); });
                }
                else Invoke((MethodInvoker)delegate { ResizeListViewColumns(lvQueryList); });
            })
            {
                IsBackground = true
            };
            t.Start(i);
        }

        private void frmShadowRdp_Resize(object sender, EventArgs e)
        {
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;

                if (WindowState != FormWindowState.Minimized)
                {
                    frmShadowRdp_ResizeEnd(sender, e);
                }
            }
        }

        private void btnForceRestart_Click(object sender, EventArgs e)
        {
            if (lvQueryList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please Select Computer from List", "Error", MessageBoxButtons.OK);
                return;
            }
            ListViewItem lvi = lvQueryList.SelectedItems[0];
            Thread t = new Thread((listviewitem) =>
            {
                ListViewItem tm = (ListViewItem)listviewitem;

                DialogResult Res = MessageBox.Show("Would you like to Force Restart " + tm.SubItems[0].Text + "?", "Conformation",
                    MessageBoxButtons.YesNo);
                if (Res == DialogResult.Yes)
                {
                    Restart(tm);
                    Invoke((MethodInvoker)delegate { lvQueryList.Items.Remove(tm); });
                }

            });
            t.IsBackground = true;
            t.Start(lvi);
        }

        
        
        private void populateVdiStatus()
        {
            statusRefresh.Stop();
            statusRefreshRunning = true;
            if (closing) { statusRefreshRunning = false; return; }
            if (!prox.ValidTicket) 
            {
                prox = new ProxAPI();
            }
            ListViewItem[] vms = prox.GetVms;
            for (int Idx = 0; Idx < vms.Length; Idx++)
            {
                ListViewItem Item = vms[Idx];

                ListViewItem tm = null;
                Invoke((MethodInvoker)delegate
                {
                    tm = (from ListViewItem l in lvVdiStatus.Items
                          where l != null && l.SubItems[0].Text.ToUpper() == Item.SubItems[0].Text.ToUpper()
                          select l).FirstOrDefault();
                });

                if (Item == null) continue;
                if (tm == null)
                {
                    Invoke((MethodInvoker)delegate { lvVdiStatus.Items.Add(Item); });
                }
                else
                {
                    List<int> updateindex = new List<int>();
                    foreach (ListViewItem.ListViewSubItem itm in Item.SubItems)
                    {
                        if (tm.SubItems[Item.SubItems.IndexOf(itm)].Text != itm.Text)
                        {
                            updateindex.Add(Item.SubItems.IndexOf(itm));
                        }
                    }
                    Invoke((MethodInvoker)delegate
                    {
                        foreach (int i in updateindex)
                        {
                            tm.SubItems[i].Text = Item.SubItems[i].Text;
                        }

                    });

                    updateindex.Clear();
                    updateindex = null;
                }

                tm = null;
                Item = null;
            }
            vms = null;
            statusRefresh.Start();
            statusRefreshRunning = false;
        }

        private void lvVdiStatus_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem lvi = lvVdiStatus.SelectedItems[0];
            Thread t = new Thread((listviewitem) =>
            {
                bool Allow = false;
                PasswordDialog pass = new PasswordDialog();
                if (pass.ShowDialog() == DialogResult.OK)
                {
                    Allow = true;
                }
                pass = null;

                if (Allow)
                {
                    ListViewItem tm = (ListViewItem)listviewitem;
                    if (tm.SubItems[3].Text != "running")
                    {
                        DialogResult Res = MessageBox.Show("Would you like to Start " + tm.SubItems[0].Text + "?", "Conformation",
                        MessageBoxButtons.YesNo);
                        if (Res == DialogResult.Yes)
                        {
                            prox.Start(tm.SubItems[2].Text, tm.SubItems[1].Text);
                        }
                    }
                    else
                    {
                        DialogResult Res = MessageBox.Show("Would you like to Restart " + tm.SubItems[0].Text + "?", "Conformation",
                        MessageBoxButtons.YesNo);
                        if (Res == DialogResult.Yes)
                        {
                            prox.Reboot(tm.SubItems[2].Text, tm.SubItems[1].Text);
                        }

                    }
                    tm = null;
                }

            });
            t.IsBackground = true;
            t.Start(lvi);
            lvi = null;
            t = null;
        }
    }
}