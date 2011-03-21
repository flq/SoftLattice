using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using SoftLattice.Common;

namespace SoftLattice.Enhancements
{
    /// <summary>
    /// Interaction logic for InteractionButton.xaml
    /// </summary>
    public partial class InteractionButton : UserControl
    {
        private readonly DoubleAnimationBase makeVisible;
        private readonly DoubleAnimationBase makeInvisible;

        private const int MillisecondsToPopupClose = 50;
        private readonly object isOpenLock = new object();
        private volatile bool isOpen;
        private volatile bool popupCloseTimerCancelled;
        private readonly Timer popupCloseTimer;

        public InteractionButton()
        {
            InitializeComponent();
            makeVisible = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(0.5)));
            makeInvisible = new DoubleAnimation(1.0, 0.0, new Duration(TimeSpan.FromSeconds(0.5)));
            popupCloseTimer = new Timer(onTimer,null, -1, -1);
            Loaded += onLoaded;
        }

        public InteractionKind KindOfInteraction { get; set; }

        public DoubleAnimationBase GradientAnimation
        {
            get { return (DoubleAnimationBase) TryFindResource("InteractionButton.GradientTimeline"); }
        }

        public void TurnOff(object token, Action<object> continuation)
        {
            makeInvisible.Completed += (s, e) => continuation(token);
            BeginAnimation(UIElement.OpacityProperty, makeInvisible);
            
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            var geometryDrawing = unfreezeGeometry();
            applyGradientAnimation(geometryDrawing);
            BeginAnimation(UIElement.OpacityProperty, makeVisible);
        }

        private GeometryDrawing unfreezeGeometry()
        {
            var b = (Brush)TryFindResource("InteractionButton." + KindOfInteraction);
            var drawingGroup = (DrawingGroup) img.Drawing;
            var dgUnfrozen = drawingGroup.Clone();
            var geometryDrawing = ((GeometryDrawing)dgUnfrozen.Children[1]);
            geometryDrawing.Brush = b.Clone();
            img.Drawing = dgUnfrozen;
            return geometryDrawing;
        }

        private void onButtonMouseEnter(object sender, MouseEventArgs e)
        {
            popupCloseTimerCancelled = true;
            popupBorder.Opacity = 0.5;
            if (isOpen)
                return;
            lock (isOpenLock)
            {
                if (isOpen) return;
                openPopup();
                isOpen = true;
            }
        }

        private void onButtonMouseLeave(object sender, MouseEventArgs e)
        {
            popupCloseTimerCancelled = false;
            popupCloseTimer.Change(MillisecondsToPopupClose, -1);
        }

        private void onPopupMouseEnter(object sender, MouseEventArgs e)
        {
            popupCloseTimerCancelled = true;
            popupBorder.Opacity = 1;
        }

        private void onTimer(object state)
        {
            if (popupCloseTimerCancelled || !isOpen)
                return;
            lock (isOpenLock)
            {
                if (popupCloseTimerCancelled || !isOpen)
                    return;
                closePopup();
            }
        }

        private void applyGradientAnimation(GeometryDrawing geometryDrawing)
        {
            var s = ((RadialGradientBrush) geometryDrawing.Brush).GradientStops[1];
            var c = GradientAnimation.CreateClock();
            s.ApplyAnimationClock(GradientStop.OffsetProperty, c);
        }

        private void openPopup()
        {
            var openStory = (Storyboard)TryFindResource("InteractionButton.Open");
            popupBorder.Opacity = 0.6;
            popup.IsOpen = true;
            openStory.Begin();
        }

        private void closePopup()
        {
            Dispatcher.Invoke(new Action(()=>
                                             {
                                                 var closeStory = (Storyboard)TryFindResource("InteractionButton.Close");
                                                 closeStory.Completed += (s, e) =>
                                                                             {
                                                                                 popup.IsOpen = false;
                                                                                 isOpen = false;
                                                                             };
                                                 closeStory.Begin();
                                             }));
        }
    }
}
