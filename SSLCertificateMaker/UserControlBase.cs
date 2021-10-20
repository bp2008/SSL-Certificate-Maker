using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace SSLCertificateMaker
{
    [Designer(typeof(UserControlSnapLineDesigner))]
    public class UserControlBase : UserControl
    {
        protected virtual Control SnapLineControl { get { return null; } }

        private class UserControlSnapLineDesigner : ControlDesigner
        {
            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    Control targetControl = (this.Control as UserControlBase).SnapLineControl;

                    if (targetControl == null)
                        return snapLines;

                    using (ControlDesigner controlDesigner = TypeDescriptor.CreateDesigner(targetControl,
                        typeof(IDesigner)) as ControlDesigner)
                    {
                        if (controlDesigner == null)
                            return snapLines;

                        controlDesigner.Initialize(targetControl);

                        foreach (SnapLine line in controlDesigner.SnapLines)
                        {
                            if (line.SnapLineType == SnapLineType.Baseline)
                            {
                                snapLines.Add(new SnapLine(SnapLineType.Baseline, line.Offset + targetControl.Top,
                                    line.Filter, line.Priority));
                                break;
                            }
                        }
                    }
                    return snapLines;
                }
            }
        }
    }
}
