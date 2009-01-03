using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;

namespace VietOCR.NET
{
    class FormLocalizer
    {
        private Form form;
        private Type formType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="form"></param>
        public FormLocalizer(Form form, Type formType)
        {
            this.form = form;
            this.formType = formType;
        }

        /// <summary>
        /// Update UI elements.
        /// Code from http://secure.codeproject.com/KB/locale/ChangeUICulture.aspx
        /// </summary>
        /// <param name="culture"></param>
        public void ApplyCulture(CultureInfo culture)
        {
            // Applies culture to current Thread.
            Thread.CurrentThread.CurrentUICulture = culture;

            // Create a resource manager for this Form
            // and determine its fields via reflection.

            ComponentResourceManager resources = new ComponentResourceManager(formType);
            FieldInfo[] fieldInfos = formType.GetFields(BindingFlags.Instance |
                BindingFlags.DeclaredOnly | BindingFlags.NonPublic);

            // Call SuspendLayout for Form and all fields derived from Control, so assignment of 
            // localized text doesn't change layout immediately.

            form.SuspendLayout();
            for (int index = 0; index < fieldInfos.Length; index++)
            {
                if (fieldInfos[index].FieldType.IsSubclassOf(typeof(Control)))
                {
                    fieldInfos[index].FieldType.InvokeMember("SuspendLayout",
                        BindingFlags.InvokeMethod, null,
                        fieldInfos[index].GetValue(form), null);
                }
            }

            // If available, assign localized text to Form and fields with Text property.

            String text = resources.GetString("$this.Text");
            if (text != null)
                form.Text = text;

            for (int index = 0; index < fieldInfos.Length; index++)
            {
                if (fieldInfos[index].FieldType.GetProperty("Text", typeof(String)) != null)
                {
                    text = resources.GetString(fieldInfos[index].Name + ".Text");
                    if (text != null)
                    {
                        fieldInfos[index].FieldType.InvokeMember("Text",
                            BindingFlags.SetProperty, null,
                            fieldInfos[index].GetValue(form), new object[] { text });
                    }
                }
            }

            // Call ResumeLayout for Form and all fields
            // derived from Control to resume layout logic.
            // Call PerformLayout, so layout changes due
            // to assignment of localized text are performed.

            for (int index = 0; index < fieldInfos.Length; index++)
            {
                if (fieldInfos[index].FieldType.IsSubclassOf(typeof(Control)))
                {
                    fieldInfos[index].FieldType.InvokeMember("ResumeLayout",
                            BindingFlags.InvokeMethod, null,
                            fieldInfos[index].GetValue(form), new object[] { false });
                }
            }
            form.ResumeLayout(false);
            form.PerformLayout();
        }
    }
}
