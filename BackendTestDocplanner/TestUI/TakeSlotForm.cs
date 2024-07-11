using BackendTestDocplanner.Services.Slot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BackendTestDocplanner.TestUI
{
    public partial class TakeSlotForm : Form
    {
        private readonly string _startDateTime;

        public TakeSlotForm(string startDateTime)
        {
            InitializeComponent();
            _startDateTime = startDateTime;

            // Display the start date and time of the selected slot
            lblStartDateTime.Text = $"Start Date and Time: {_startDateTime}";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the values entered by the user
            string comments = txtComments.Text;
            string name = txtName.Text;
            string secondName = txtSecondName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;

            // Create the PatientModel object
            Patient patient = new Patient(name, secondName, email, phone);

            // Create the TakeASlotRequest object
            TakeSlotRequest takeSlotRequest = new TakeSlotRequest(_startDateTime, null, comments, patient);

            // Here you should perform any additional logic, such as sending the object to a service or storing it somewhere

            // Display confirmation message
            MessageBox.Show("Information saved successfully:\n" +
                            $"Start Date and Time: {takeSlotRequest.Start}\n" +
                            $"Comments: {takeSlotRequest.Comments}\n" +
                            $"Patient: {takeSlotRequest.Patient.Name} {takeSlotRequest.Patient.SecondName}\n" +
                            $"Email: {takeSlotRequest.Patient.Email}\n" +
                            $"Phone: {takeSlotRequest.Patient.Phone}");

            // Close the form with DialogResult OK to indicate that the operation was successful
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
