namespace AirTransit_WindowsForms
{
    partial class AirTransit
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AirTransit));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnContact = new System.Windows.Forms.Button();
            this.ListContacts = new System.Windows.Forms.ListBox();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.Txtconversation = new System.Windows.Forms.RichTextBox();
            this.TxtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtConnectedPhone = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnContact);
            this.groupBox1.Controls.Add(this.ListContacts);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 477);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contacts";
            // 
            // BtnContact
            // 
            this.BtnContact.Location = new System.Drawing.Point(7, 448);
            this.BtnContact.Name = "BtnContact";
            this.BtnContact.Size = new System.Drawing.Size(199, 23);
            this.BtnContact.TabIndex = 1;
            this.BtnContact.Text = "Add Contact";
            this.BtnContact.UseVisualStyleBackColor = true;
            this.BtnContact.Click += new System.EventHandler(this.BtnContact_Click);
            // 
            // ListContacts
            // 
            this.ListContacts.FormattingEnabled = true;
            this.ListContacts.Location = new System.Drawing.Point(7, 20);
            this.ListContacts.Name = "ListContacts";
            this.ListContacts.Size = new System.Drawing.Size(199, 420);
            this.ListContacts.Sorted = true;
            this.ListContacts.TabIndex = 0;
            this.ListContacts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListContacts_MouseClick);
            this.ListContacts.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListContacts_MouseDoubleClick);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(861, 24);
            this.MenuStrip.TabIndex = 1;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnSend);
            this.groupBox2.Controls.Add(this.Txtconversation);
            this.groupBox2.Controls.Add(this.TxtInput);
            this.groupBox2.Location = new System.Drawing.Point(230, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(619, 477);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Messages";
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(565, 425);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(48, 46);
            this.BtnSend.TabIndex = 2;
            this.BtnSend.Text = "Send";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // Txtconversation
            // 
            this.Txtconversation.Location = new System.Drawing.Point(6, 19);
            this.Txtconversation.Name = "Txtconversation";
            this.Txtconversation.Size = new System.Drawing.Size(607, 399);
            this.Txtconversation.TabIndex = 1;
            this.Txtconversation.Text = "";
            // 
            // TxtInput
            // 
            this.TxtInput.Location = new System.Drawing.Point(6, 424);
            this.TxtInput.Multiline = true;
            this.TxtInput.Name = "TxtInput";
            this.TxtInput.Size = new System.Drawing.Size(552, 47);
            this.TxtInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Connected as: ";
            // 
            // TxtConnectedPhone
            // 
            this.TxtConnectedPhone.AutoSize = true;
            this.TxtConnectedPhone.Location = new System.Drawing.Point(104, 27);
            this.TxtConnectedPhone.Name = "TxtConnectedPhone";
            this.TxtConnectedPhone.Size = new System.Drawing.Size(67, 13);
            this.TxtConnectedPhone.TabIndex = 4;
            this.TxtConnectedPhone.Text = "0000000000";
            // 
            // AirTransit
            // 
            this.AcceptButton = this.BtnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 535);
            this.Controls.Add(this.TxtConnectedPhone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "AirTransit";
            this.Text = "AirTransit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AirTransit_FormClosing);
            this.Load += new System.EventHandler(this.AirTransit_Load);
            this.groupBox1.ResumeLayout(false);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox Txtconversation;
        private System.Windows.Forms.TextBox TxtInput;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.ListBox ListContacts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TxtConnectedPhone;
        private System.Windows.Forms.Button BtnContact;
    }
}

