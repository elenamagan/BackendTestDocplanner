namespace BackendTestDocplanner
{
    partial class WeeklySlots
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button testButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.testButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(350, 200);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(100, 50);
            this.testButton.TabIndex = 0;
            this.testButton.Text = "Test it!";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);

            // 
            // WeeklySlots
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.testButton);
            this.Text = "Weekly Slots";
            this.ResumeLayout(false);
        }

        #endregion
    }
}