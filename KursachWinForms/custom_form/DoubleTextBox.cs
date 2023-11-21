using System;
using System.Windows.Forms;

using KursachWinForms.Controller;

namespace KursachWinForms.CustomForm
{
    internal class DoubleTextBox: TextBox
    {
        private ComponentMenu componentMenu;
        private Form main_form;
        private string key;
        public double value;

        public DoubleTextBox()
        {
            key = "";
            componentMenu = null;
            main_form = null;
            value = 0;
            this.AppendText("0");
            this.KeyPress += DoubleTextBox_KeyPress;
            this.KeyDown += SetDoubleResult;
        }
        public DoubleTextBox(Form main_form, ComponentMenu componentMenu, double value, string key)
        {
            this.key = key;
            this.componentMenu = componentMenu;
            this.main_form = main_form;
            this.main_form.ActiveControl = null;
            this.value = value;
            this.AppendText(value.ToString());
            this.KeyPress += DoubleTextBox_KeyPress;
            this.KeyDown += SetDoubleResult;
        }

        private void DoubleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void SetDoubleResult (object sender, KeyEventArgs e)
        {
            if((int)e.KeyCode == (int)KeyValue.Agree || (int)e.KeyCode == (int)KeyValue.ClearAll)
            {
                if (Double.TryParse((sender as TextBox).Text, out value))
                {
                    if (main_form == null)
                        this.FindForm().ActiveControl = null;
                    else
                        main_form.ActiveControl = null;

                    value = value<-100 ? -100 : value;
                    value = value > 100 ? 100 : value;
                    (sender as TextBox).Text = value.ToString();

                    if (componentMenu != null) componentMenu.UpdateEObject(key,value);

                    e.Handled = true;
                }
            }
        }

    }
}
