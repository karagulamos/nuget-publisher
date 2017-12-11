// Author: Alexander Karagulamos
// Date:
// Comment: Custom forms control manager that dynamically disables form controls prior to running a task and re-enables them when done.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NugetPublisher.Desktop.UI
{
    public class FormControlManager
    {
        private readonly HashSet<Control> _controls;

        public FormControlManager(Control parent)
        {
            _controls = new HashSet<Control>();

            foreach (var control in parent.Controls)
            {
                _controls.Add((Control)control);
            }
        }

        public async Task ToggleControlsOfType<T>(Func<Task> action, HashSet<T> excludedControls = null) where T : Control
        {
            var controlQuery = new ControlBuilder<T>(_controls.OfType<T>()).ExcludeControls(excludedControls);

            try
            {
                controlQuery.Disable();
                await action.Invoke();
            }
            finally
            {
                controlQuery.Enable();
            }
        }

        private class ControlBuilder<T> where T : Control
        {
            private readonly IEnumerable<T> _controls;

            public ControlBuilder(IEnumerable<T> controls)
            {
                _controls = controls;
            }
            
            public ControlBuilder<T> ExcludeControls(ICollection<T> controls)
            {
                if (controls == null || controls.Count == 0)
                    return this;

                var filteredControls = _controls.Where(control => !controls.Contains(control));

                return new ControlBuilder<T>(filteredControls);
            }

            public void Disable()
            {
                foreach (var control in _controls)
                {
                    control.Enabled = false;
                }
            }

            public void Enable()
            {
                foreach (var control in _controls)
                {
                    control.Enabled = true;
                }
            }
        }
    }
}
