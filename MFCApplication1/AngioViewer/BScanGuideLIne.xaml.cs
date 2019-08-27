using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AngioViewer
{
    /// <summary>
    /// Interaction logic for BScanGuideLIne.xaml
    /// </summary>
    public partial class BScanGuideLine : UserControl
    {
        public event Action<int, int> ScanIndexChanged;

        private Color kColorHor = Color.FromRgb(255, 252, 0);
        private Color kColorVer = Color.FromRgb(0, 255, 255);

        public BScanGuideLine()
        {
            InitializeComponent();

            IsVertical = false;

            PreviewMouseDown += BScanGuideLine_PreviewMouseDown;
            PreviewMouseMove += BScanGuideLine_PreviewMouseMove;
            PreviewMouseUp += BScanGuideLine_PreviewMouseUp;
        }

        public void setLineIndex(int curValue, int maxValue)
        {
            double linePosition;
            double dispWidth;
            if (IsVertical)
            {
                dispWidth = Width;
            }
            else
            {
                dispWidth = Height;
            }

            linePosition = dispWidth / (double)maxValue * (double)curValue;

            drawLine((int)linePosition);
        }

        private void drawLine(int linePos)
        {
            int minPos, maxPos;

            minPos = 0;
            if (linePos < minPos)
            {
                linePos = minPos;
            }

            if (IsVertical)
            {
                maxPos = (int)Width;
                if (linePos >= maxPos)
                {
                    linePos = maxPos - 1;
                }

                line.X1 = linePos;
                line.Y1 = 0;
                line.X2 = linePos;
                line.Y2 = Height;
                line.Stroke = new SolidColorBrush(kColorVer);
            }
            else
            {
                maxPos = (int)Height;
                if (linePos >= maxPos)
                {
                    linePos = maxPos - 1;
                }

                line.X1 = 0;
                line.Y1 = linePos;
                line.X2 = Width;
                line.Y2 = linePos;
                line.Stroke = new SolidColorBrush(kColorHor);
            }
        }

        private void BScanGuideLine_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // release this control.
            this.ReleaseMouseCapture();
        }

        private void BScanGuideLine_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // if the mouse is captured. you are moving it. (there is your 'real' boolean)
            if (this.IsMouseCaptured)
            {
                int linePosition;
                var mousePos = Mouse.GetPosition(this);

                if (IsVertical)
                {
                    linePosition = Math.Min(Math.Max(0, (int)mousePos.X), (int)Width);
                    ScanIndexChanged(linePosition, (int)Width);
                }
                else
                {
                    linePosition = Math.Min(Math.Max(0, (int)mousePos.Y), (int)Height);
                    ScanIndexChanged(linePosition, (int)Height);
                }
            }
        }

        private void BScanGuideLine_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // capture the mouse (so the mouse move events are still triggered (even when the mouse is not above the control)
            this.CaptureMouse();
        }

        public bool IsVertical { get; set; }
    }
}
