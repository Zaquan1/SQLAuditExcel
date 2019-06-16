using FileManagerV1.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FileManagerV1.VIewModel
{
    public class WindowViewModel : BaseViewModel
    {
        #region Private Member

        private Window mWindow;

        private int mOuterMargineSize = 10;
        private int mWindowRadius = 10;

        #endregion

        #region Public Properties

        public double WIndowMinimumWidth { get; set; } = 400;

        public double WindowMinimumHeight { get; set; } = 400;

        public int ResizeBorder { get; set; } = 5;

        public Thickness ResizeBorderThickness
        { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

        public Thickness InnerContentPadding
        { get { return new Thickness(ResizeBorder); } }

        public int OuterMarginSize
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMargineSize;
            }
            set
            {
                mOuterMargineSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness
        { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius
        { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 30;

        public GridLength TitleHeightGridLength
        { get { return new GridLength(TitleHeight + ResizeBorder); } }


        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }


        #endregion

        #region Constructor

        public WindowViewModel(Window window)

        {
            mWindow = window;

            //Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                //Fire off events for all properties that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));
            };

            //Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            var resizer = new WindowResizer(mWindow);
        }

        #endregion

        #region Private Helper

        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);

            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion
    }
}
