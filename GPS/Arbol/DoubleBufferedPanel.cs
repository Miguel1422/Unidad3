using System.Windows.Forms;

namespace Gps.Arbol
{
    public class DoubleBufferedPanel : Panel
    {

        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }

        public new bool DoubleBuffered
        {
            get
            {
                return base.DoubleBuffered;
            }
            set
            {
                base.DoubleBuffered = value;
            }
        }
    }
}
