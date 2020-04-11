using System;
using System.Text;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using BloodCompat.Models;

namespace BloodCompat
{
    class MainWindow : Window
    {
        // Combo box for donor: Antigen & RH
        [UI] private ComboBox _donor_blood_antigen = null;

        [UI] private ComboBox _donor_blood_rh = null;

        // Combo box for recipient: Antigen & RH
        [UI] private ComboBox _recipient_blood_antigen = null;

        [UI] private ComboBox _recipient_blood_rh = null;
        [UI] private Button _result_button = null;

        [UI] private Label _plasma_label = null;

        [UI] private Label _rbc_label = null;

        // Cell Renderers for Project.
        CellRendererText[] comboBoxRenderers = {
            new CellRendererText(),
            new CellRendererText(),
            new CellRendererText(),
            new CellRendererText()
        };
        private ListStore donorAntigens = new ListStore(typeof (string));
        private ListStore donorRHs = new ListStore(typeof (string));
        private ListStore recipientAntigens = new ListStore(typeof (string));
        private ListStore recipientRHs = new ListStore(typeof (string));


        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
            _result_button.Clicked += Result_Button_Clicked;
            InitializeCellRenderers();
            PopulateComboBoxes();
        }

        // Clear all combobox values
        public void ClearComboBoxes()
        {
            _donor_blood_antigen.Clear();
            _donor_blood_rh.Clear();
            _recipient_blood_antigen.Clear();
            _recipient_blood_rh.Clear();
        }

        public void InitializeCellRenderers()
        {
            _donor_blood_antigen.PackStart(comboBoxRenderers[0], false);
            _donor_blood_antigen.AddAttribute(comboBoxRenderers[0], "text", 0);
            _donor_blood_rh.PackStart(comboBoxRenderers[1], false);
            _donor_blood_rh.AddAttribute(comboBoxRenderers[1], "text", 0);
            _recipient_blood_antigen.PackStart(comboBoxRenderers[2], false);
            _recipient_blood_antigen.AddAttribute(comboBoxRenderers[2], "text", 0);
            _recipient_blood_rh.PackStart(comboBoxRenderers[3], false);
            _recipient_blood_rh.AddAttribute(comboBoxRenderers[3], "text", 0);
            _donor_blood_antigen.Model = donorAntigens;
            _donor_blood_rh.Model = donorRHs;
            _recipient_blood_antigen.Model = recipientAntigens;
            _recipient_blood_rh.Model = recipientRHs;
        }

        // Populate ComboBoxes
        public void PopulateComboBoxes()
        {
            string[] antigen_values = {"O", "A", "B", "AB"};
            string[] rh_values = { "-ve", "+ve" };
            foreach(string antigen in antigen_values) {
                donorAntigens.AppendValues(antigen);
                recipientAntigens.AppendValues(antigen);
            }
            foreach(string rh in rh_values) {
                donorRHs.AppendValues(rh);
                recipientRHs.AppendValues(rh);
            }

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void Result_Button_Clicked(object sender, EventArgs a)
        {
            Blood donor = new Blood((BloodAntigen)_donor_blood_antigen.Active, (BloodRH)_donor_blood_rh.Active);
            Blood recipient = new Blood((BloodAntigen)_recipient_blood_antigen.Active, (BloodRH)_recipient_blood_rh.Active);
            // Console.WriteLine(donor);
            // Console.WriteLine(recipient);
            BloodCompatibility rbcCompatibility = donor.isRBCCompatible(recipient);
            BloodCompatibility plasmaCompatibility = donor.isPlasmaCompatible(recipient);
            switch (rbcCompatibility) {
                case BloodCompatibility.ERROR:
                    _rbc_label.Text = "Error in Calculating RBC Compatibility.";
                    break;
                case BloodCompatibility.COMPATIBLE:
                    _rbc_label.Text = "RBC is Compatible";
                    break;
                case BloodCompatibility.INCOMPATIBLE:
                    _rbc_label.Text = "RBC is Incompatible";
                    break;
                default:
                    _rbc_label.Text = "Unknown Result";
                    break;
            }
            
            switch (plasmaCompatibility) {
                case BloodCompatibility.ERROR:
                    _plasma_label.Text = "Error in Calculating Plasma Compatibility";
                    break;
                case BloodCompatibility.COMPATIBLE:
                    _plasma_label.Text = "Plasma is Compatible";
                    break;
                case BloodCompatibility.INCOMPATIBLE:
                    _plasma_label.Text = "Plasma is Incompatible";
                    break;
                default:
                    _plasma_label.Text = "Unknown Result";
                    break;
            };
        }
    }
}
