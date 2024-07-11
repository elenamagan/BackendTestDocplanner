namespace BackendTestDocplanner.TestUI
{
    partial class TakeSlotForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblStartDateTime;
        private TextBox txtComments;
        private TextBox txtName;
        private TextBox txtSecondName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblStartDateTime = new Label();
            this.txtComments = new TextBox();
            this.txtName = new TextBox();
            this.txtSecondName = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPhone = new TextBox();
            this.btnSave = new Button();
            this.SuspendLayout();

            // 
            // lblStartDateTime
            // 
            this.lblStartDateTime.AutoSize = true;
            this.lblStartDateTime.Location = new Point(12, 9);
            this.lblStartDateTime.Name = "lblStartDateTime";
            this.lblStartDateTime.Size = new Size(160, 15);
            this.lblStartDateTime.TabIndex = 0;
            this.lblStartDateTime.Text = "Start Date and Time:";

            // 
            // txtComments
            // 
            this.txtComments.Location = new Point(12, 36);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new Size(260, 60);
            this.txtComments.TabIndex = 1;
            this.txtComments.PlaceholderText = "Additional Comments";

            // 
            // txtName
            // 
            this.txtName.Location = new Point(12, 102);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(260, 23);
            this.txtName.TabIndex = 2;
            this.txtName.PlaceholderText = "First Name";

            // 
            // txtSecondName
            // 
            this.txtSecondName.Location = new Point(12, 131);
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new Size(260, 23);
            this.txtSecondName.TabIndex = 3;
            this.txtSecondName.PlaceholderText = "Last Name";

            // 
            // txtEmail
            // 
            this.txtEmail.Location = new Point(12, 160);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(260, 23);
            this.txtEmail.TabIndex = 4;
            this.txtEmail.PlaceholderText = "Email";

            // 
            // txtPhone
            // 
            this.txtPhone.Location = new Point(12, 189);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new Size(260, 23);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.PlaceholderText = "Phone";

            // 
            // btnSave
            // 
            this.btnSave.Location = new Point(197, 218);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // 
            // TakeSlotForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(284, 261);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSecondName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.lblStartDateTime);
            this.Name = "TakeSlotForm";
            this.Text = "Take Slot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}