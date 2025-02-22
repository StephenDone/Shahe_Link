namespace Shahe_Link
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpAdvert = new GroupBox();
            txtGaugeName = new TextBox();
            chkAutoConnect = new CheckBox();
            btnDisconnect = new Button();
            btnConnect = new Button();
            lbAdverts = new ListBox();
            grpGaugeDisplay = new GroupBox();
            chkOnTop = new CheckBox();
            chkSpeak = new CheckBox();
            chkNegate = new CheckBox();
            txtValue = new TextBox();
            grpHotKey = new GroupBox();
            txtHotKeyControlLocation = new TextBox();
            chkWinKey = new CheckBox();
            txtHotKeyValue = new TextBox();
            grpCaptureFormat = new GroupBox();
            tbAfterValue = new TextBox();
            tbBeforeValue = new TextBox();
            lblAfterValue = new Label();
            lblBeforeValue = new Label();
            llSendFormat = new LinkLabel();
            grpAdvert.SuspendLayout();
            grpGaugeDisplay.SuspendLayout();
            grpHotKey.SuspendLayout();
            grpCaptureFormat.SuspendLayout();
            SuspendLayout();
            // 
            // grpAdvert
            // 
            grpAdvert.Controls.Add(txtGaugeName);
            grpAdvert.Controls.Add(chkAutoConnect);
            grpAdvert.Controls.Add(btnDisconnect);
            grpAdvert.Controls.Add(btnConnect);
            grpAdvert.Controls.Add(lbAdverts);
            grpAdvert.Location = new Point(8, 0);
            grpAdvert.Name = "grpAdvert";
            grpAdvert.Size = new Size(374, 210);
            grpAdvert.TabIndex = 1;
            grpAdvert.TabStop = false;
            grpAdvert.Text = "Bluetooth Gauge Connection";
            // 
            // txtGaugeName
            // 
            txtGaugeName.Location = new Point(21, 163);
            txtGaugeName.Name = "txtGaugeName";
            txtGaugeName.PlaceholderText = "Last Selected Gauge";
            txtGaugeName.ReadOnly = true;
            txtGaugeName.Size = new Size(193, 31);
            txtGaugeName.TabIndex = 5;
            // 
            // chkAutoConnect
            // 
            chkAutoConnect.AutoSize = true;
            chkAutoConnect.Location = new Point(220, 165);
            chkAutoConnect.Name = "chkAutoConnect";
            chkAutoConnect.Size = new Size(147, 29);
            chkAutoConnect.TabIndex = 4;
            chkAutoConnect.Text = "Auto Connect";
            chkAutoConnect.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Location = new Point(220, 70);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(111, 34);
            btnDisconnect.TabIndex = 2;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.UseVisualStyleBackColor = true;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(220, 30);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(111, 34);
            btnConnect.TabIndex = 1;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            // 
            // lbAdverts
            // 
            lbAdverts.FormattingEnabled = true;
            lbAdverts.ItemHeight = 25;
            lbAdverts.Location = new Point(21, 30);
            lbAdverts.Name = "lbAdverts";
            lbAdverts.Size = new Size(193, 129);
            lbAdverts.TabIndex = 3;
            // 
            // grpGaugeDisplay
            // 
            grpGaugeDisplay.Controls.Add(chkOnTop);
            grpGaugeDisplay.Controls.Add(chkSpeak);
            grpGaugeDisplay.Controls.Add(chkNegate);
            grpGaugeDisplay.Controls.Add(txtValue);
            grpGaugeDisplay.Location = new Point(388, 0);
            grpGaugeDisplay.Name = "grpGaugeDisplay";
            grpGaugeDisplay.Size = new Size(244, 210);
            grpGaugeDisplay.TabIndex = 2;
            grpGaugeDisplay.TabStop = false;
            grpGaugeDisplay.Text = "Gauge Display";
            // 
            // chkOnTop
            // 
            chkOnTop.AutoSize = true;
            chkOnTop.Location = new Point(22, 165);
            chkOnTop.Name = "chkOnTop";
            chkOnTop.Size = new Size(156, 29);
            chkOnTop.TabIndex = 3;
            chkOnTop.Text = "Always On Top";
            chkOnTop.UseVisualStyleBackColor = true;
            chkOnTop.CheckedChanged += chkOnTop_CheckedChanged;
            // 
            // chkSpeak
            // 
            chkSpeak.AutoSize = true;
            chkSpeak.Location = new Point(22, 135);
            chkSpeak.Name = "chkSpeak";
            chkSpeak.Size = new Size(141, 29);
            chkSpeak.TabIndex = 2;
            chkSpeak.Text = "Speak Values";
            chkSpeak.UseVisualStyleBackColor = true;
            // 
            // chkNegate
            // 
            chkNegate.AutoSize = true;
            chkNegate.Location = new Point(22, 107);
            chkNegate.Name = "chkNegate";
            chkNegate.Size = new Size(142, 29);
            chkNegate.TabIndex = 1;
            chkNegate.Text = "Negate Value";
            chkNegate.UseVisualStyleBackColor = true;
            // 
            // txtValue
            // 
            txtValue.BackColor = Color.DimGray;
            txtValue.BorderStyle = BorderStyle.None;
            txtValue.Font = new Font("Lucida Console", 32F, FontStyle.Bold);
            txtValue.Location = new Point(22, 30);
            txtValue.Name = "txtValue";
            txtValue.ReadOnly = true;
            txtValue.Size = new Size(197, 64);
            txtValue.TabIndex = 0;
            txtValue.TextAlign = HorizontalAlignment.Right;
            // 
            // grpHotKey
            // 
            grpHotKey.Controls.Add(txtHotKeyControlLocation);
            grpHotKey.Controls.Add(chkWinKey);
            grpHotKey.Controls.Add(txtHotKeyValue);
            grpHotKey.Location = new Point(8, 216);
            grpHotKey.Name = "grpHotKey";
            grpHotKey.Size = new Size(309, 150);
            grpHotKey.TabIndex = 3;
            grpHotKey.TabStop = false;
            grpHotKey.Text = "Value Capture Hot Key";
            // 
            // txtHotKeyControlLocation
            // 
            txtHotKeyControlLocation.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtHotKeyControlLocation.Location = new Point(21, 30);
            txtHotKeyControlLocation.Name = "txtHotKeyControlLocation";
            txtHotKeyControlLocation.Size = new Size(269, 31);
            txtHotKeyControlLocation.TabIndex = 2;
            txtHotKeyControlLocation.Text = "HotKey Control Location";
            // 
            // chkWinKey
            // 
            chkWinKey.AutoSize = true;
            chkWinKey.Location = new Point(21, 69);
            chkWinKey.Name = "chkWinKey";
            chkWinKey.Size = new Size(184, 29);
            chkWinKey.TabIndex = 1;
            chkWinKey.Text = "Add Windows Key";
            chkWinKey.UseVisualStyleBackColor = true;
            // 
            // txtHotKeyValue
            // 
            txtHotKeyValue.Font = new Font("Lucida Console", 9F);
            txtHotKeyValue.Location = new Point(21, 110);
            txtHotKeyValue.Name = "txtHotKeyValue";
            txtHotKeyValue.Size = new Size(269, 25);
            txtHotKeyValue.TabIndex = 0;
            txtHotKeyValue.Text = "HotKey Value";
            txtHotKeyValue.Visible = false;
            // 
            // grpCaptureFormat
            // 
            grpCaptureFormat.Controls.Add(tbAfterValue);
            grpCaptureFormat.Controls.Add(tbBeforeValue);
            grpCaptureFormat.Controls.Add(lblAfterValue);
            grpCaptureFormat.Controls.Add(lblBeforeValue);
            grpCaptureFormat.Controls.Add(llSendFormat);
            grpCaptureFormat.Location = new Point(323, 216);
            grpCaptureFormat.Name = "grpCaptureFormat";
            grpCaptureFormat.Size = new Size(309, 150);
            grpCaptureFormat.TabIndex = 4;
            grpCaptureFormat.TabStop = false;
            grpCaptureFormat.Text = "Captured Value Format";
            // 
            // tbAfterValue
            // 
            tbAfterValue.Location = new Point(87, 67);
            tbAfterValue.Name = "tbAfterValue";
            tbAfterValue.PlaceholderText = "Keystrokes after value";
            tbAfterValue.Size = new Size(216, 31);
            tbAfterValue.TabIndex = 4;
            tbAfterValue.Text = "{ENTER}";
            // 
            // tbBeforeValue
            // 
            tbBeforeValue.Location = new Point(87, 30);
            tbBeforeValue.Name = "tbBeforeValue";
            tbBeforeValue.PlaceholderText = "Keystrokes before value";
            tbBeforeValue.Size = new Size(216, 31);
            tbBeforeValue.TabIndex = 3;
            // 
            // lblAfterValue
            // 
            lblAfterValue.AutoSize = true;
            lblAfterValue.Location = new Point(22, 70);
            lblAfterValue.Name = "lblAfterValue";
            lblAfterValue.Size = new Size(51, 25);
            lblAfterValue.TabIndex = 2;
            lblAfterValue.Text = "After";
            // 
            // lblBeforeValue
            // 
            lblBeforeValue.AutoSize = true;
            lblBeforeValue.Location = new Point(22, 33);
            lblBeforeValue.Name = "lblBeforeValue";
            lblBeforeValue.Size = new Size(63, 25);
            lblBeforeValue.TabIndex = 1;
            lblBeforeValue.Text = "Before";
            // 
            // llSendFormat
            // 
            llSendFormat.AutoSize = true;
            llSendFormat.Location = new Point(22, 107);
            llSendFormat.Name = "llSendFormat";
            llSendFormat.Size = new Size(178, 25);
            llSendFormat.TabIndex = 0;
            llSendFormat.TabStop = true;
            llSendFormat.Text = "Capture Format Help";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 378);
            Controls.Add(grpCaptureFormat);
            Controls.Add(grpHotKey);
            Controls.Add(grpGaugeDisplay);
            Controls.Add(grpAdvert);
            Name = "Form1";
            Text = "Shahe Gauge Link - Stephen Done 2024";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            grpAdvert.ResumeLayout(false);
            grpAdvert.PerformLayout();
            grpGaugeDisplay.ResumeLayout(false);
            grpGaugeDisplay.PerformLayout();
            grpHotKey.ResumeLayout(false);
            grpHotKey.PerformLayout();
            grpCaptureFormat.ResumeLayout(false);
            grpCaptureFormat.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpAdvert;
        private Button btnDisconnect;
        private Button btnConnect;
        private ListBox lbAdverts;
        private GroupBox grpGaugeDisplay;
        private TextBox txtValue;
        private CheckBox chkNegate;
        private CheckBox chkSpeak;
        private GroupBox grpHotKey;
        private GroupBox grpCaptureFormat;
        private LinkLabel llSendFormat;
        private Label lblAfterValue;
        private Label lblBeforeValue;
        private TextBox tbAfterValue;
        private TextBox tbBeforeValue;
        private TextBox txtHotKeyValue;
        private CheckBox chkWinKey;
        private TextBox txtHotKeyControlLocation;
        private CheckBox chkAutoConnect;
        private TextBox txtGaugeName;
        private CheckBox chkOnTop;
    }
}
