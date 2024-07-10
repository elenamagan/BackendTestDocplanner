using BackendTestDocplanner.Controllers;

namespace BackendTestDocplanner
{
    partial class WeeklySlots
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnPreviousWeek;
        private System.Windows.Forms.Button btnNextWeek;
        private Label titleLabel;

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
            btnPreviousWeek = new Button();
            btnNextWeek = new Button();

            // Añade el título
            titleLabel = new Label
            {
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.BottomRight,
                BackColor = Color.Transparent,
                Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold),
                Text = $"Available slots from {SlotController.GetWeekStartDate(_currentDate).ToString("dd/MM/yyyy")} to {SlotController.GetWeekEndDate(_currentDate).ToString("dd/MM/yyyy")}"
            };

            this.Controls.Add(titleLabel);

            // 
            // btnPreviousWeek
            // 
            btnPreviousWeek.Location = new Point(12, 223);
            btnPreviousWeek.Name = "btnPreviousWeek";
            btnPreviousWeek.Size = new Size(35, 28);
            btnPreviousWeek.TabIndex = 1;
            btnPreviousWeek.Text = "<";
            btnPreviousWeek.UseVisualStyleBackColor = true;
            btnPreviousWeek.Click += BtnPreviousWeek_Click;
            btnPreviousWeek.Enabled = false;
            // 
            // btnNextWeek
            // 
            btnNextWeek.Location = new Point(1235, 223);
            btnNextWeek.Name = "btnNextWeek";
            btnNextWeek.Size = new Size(35, 28);
            btnNextWeek.TabIndex = 0;
            btnNextWeek.Text = ">";
            btnNextWeek.UseVisualStyleBackColor = true;
            btnNextWeek.Click += BtnNextWeek_Click;

            // 
            // WeeklySlots
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1282, 564);
            Controls.Add(btnNextWeek);
            Controls.Add(btnPreviousWeek);
            this.Padding = new System.Windows.Forms.Padding(60); // Añade un padding de 40px
            Name = "WeeklySlots";
            Text = "Weekly Slots";
        }

        #endregion
    }
}