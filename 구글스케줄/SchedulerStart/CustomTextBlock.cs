using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SchedulerStart
{
    class CustomTextBlock : TextBlock
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomTextBlock));
        public static readonly RoutedEvent DoubleClickEvent = EventManager.RegisterRoutedEvent("DoubleClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomTextBlock));

        bool MouseDowned = false;
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }
        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CustomTextBlock.ClickEvent);
            RaiseEvent(newEventArgs);
        }

        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            MouseDowned = true;

        }

        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (MouseDowned)
            {
                MouseDowned = false;
                this.Background = null;
            }
        }
        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (MouseDowned)
            {
                RaiseClickEvent();
                if (this.Background != SystemColors.ActiveBorderBrush)
                {
                    this.Background = SystemColors.ActiveBorderBrush;
                }
                else
                {
                    RaiseDoubleClickEvent();
                    this.Background = null;
                }
            }
        }
        public event RoutedEventHandler DoubleClick
        {
            add { AddHandler(DoubleClickEvent, value); }
            remove { RemoveHandler(DoubleClickEvent, value); }
        }

        void RaiseDoubleClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(CustomTextBlock.DoubleClickEvent);
            MessageBox.Show("Doubleclick");
            RaiseEvent(newEventArgs);
        }

    }
}
