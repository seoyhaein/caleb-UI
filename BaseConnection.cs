using Avalonia.Controls.Shapes;
using Avalonia;
using Avalonia.Media;
using Avalonia.Input;

using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CalebUI
{
    // https://docs.avaloniaui.net/misc/wpf/uielement-frameworkelement-and-control
    // http://reference.avaloniaui.net/api/Avalonia/Vector/
    // http://reference.avaloniaui.net/api/Avalonia.Media/StreamGeometry/

    // TODO
    // https://stackoverflow.com/questions/66386895/avaloniaui-capture-mouse-button-up-down-globally

    // mouse button down and up event 
    // shape 에서 특정 컨트롤에 해당 이벤트를 넣고 이것을 처리하는 방식을 간단히 테스트 해본다.

    // https://www.youtube.com/watch?v=x1z5zbUgP90

    // https://marketplace.visualstudio.com/items?itemName=AvaloniaTeam.AvaloniaVS&ssr=false#qna


    // dotnet 에서 interface
    // https://holjjack.tistory.com/31

    /// <summary>
    /// Specifies the offset type that can be applied to a <see cref="BaseConnection"/> using the <see cref="BaseConnection.SourceOffset"/> and the <see cref="BaseConnection.TargetOffset"/> values.
    /// </summary>
    public enum ConnectionOffsetMode
    {
        /// <summary>
        /// No offset applied.
        /// </summary>
        None,

        /// <summary>
        /// The offset is applied in a circle around the point.
        /// </summary>
        Circle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point.
        /// </summary>
        Rectangle,

        /// <summary>
        /// The offset is applied in a rectangle shape around the point, perpendicular to the edges.
        /// </summary>
        Edge,
    }

    /// <summary>
    /// The direction in which a connection is oriented.
    /// </summary>
    public enum ConnectionDirection
    {
        /// <summary>
        /// From <see cref="BaseConnection.Source"/> to <see cref="BaseConnection.Target"/>.
        /// </summary>
        Forward,

        /// <summary>
        /// From <see cref="BaseConnection.Target"/> to <see cref="BaseConnection.Source"/>.
        /// </summary>
        Backward
    }

    /// <summary>
    /// The end at which the arrow head is drawn.
    /// </summary>
    public enum ArrowHeadEnds
    {
        /// <summary>
        /// Arrow head at start.
        /// </summary>
        Start,

        /// <summary>
        /// Arrow head at end.
        /// </summary>
        End,

        /// <summary>
        /// Arrow heads at both ends.
        /// </summary>
        Both,

        /// <summary>
        /// No arrow head.
        /// </summary>
        None
    }


    public class BaseConnection : Shape
    { 
        #region Dependency Properties
        // 일단, StyledProperty 로 진행하고 추후에 DirectProperty 를 살펴보자.

        //public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(nameof(Target), typeof(Point), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Point, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty SourceOffsetProperty = DependencyProperty.Register(nameof(SourceOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty TargetOffsetProperty = DependencyProperty.Register(nameof(TargetOffset), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Size, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty OffsetModeProperty = DependencyProperty.Register(nameof(OffsetMode), typeof(ConnectionOffsetMode), typeof(BaseConnection), new FrameworkPropertyMetadata(default(ConnectionOffsetMode), FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(nameof(Direction), typeof(ConnectionDirection), typeof(BaseConnection), new FrameworkPropertyMetadata(default(ConnectionDirection), FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty ArrowHeadEndsProperty = DependencyProperty.Register(nameof(ArrowEnds), typeof(ArrowHeadEnds), typeof(BaseConnection), new FrameworkPropertyMetadata(ArrowHeadEnds.End, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.Double0, FrameworkPropertyMetadataOptions.AffectsRender));
        //public static readonly DependencyProperty ArrowSizeProperty = DependencyProperty.Register(nameof(ArrowSize), typeof(Size), typeof(BaseConnection), new FrameworkPropertyMetadata(BoxValue.ArrowSize, FrameworkPropertyMetadataOptions.AffectsRender));
        
        // 아래 두개는 일단 확인하자.
        //public static readonly DependencyProperty SplitCommandProperty = DependencyProperty.Register(nameof(SplitCommand), typeof(ICommand), typeof(BaseConnection));
        //public static readonly DependencyProperty DisconnectCommandProperty = Connector.DisconnectCommandProperty.AddOwner(typeof(BaseConnection));


        /// <summary>
        /// Defines the <see cref="SourceProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<Point> SourceProperty =
            AvaloniaProperty.Register<BaseConnection, Point>(nameof(Source));

        /// <summary>
        /// Defines the <see cref="TargetProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<Point> TargetProperty =
            AvaloniaProperty.Register<BaseConnection, Point>(nameof(Target));


        /// <summary>
        /// Defines the <see cref="SourceOffsetProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<Size> SourceOffsetProperty =
            AvaloniaProperty.Register<BaseConnection, Size>(nameof(SourceOffset));

        /// <summary>
        /// Defines the <see cref="TargetOffsetProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<Size> TargetOffsetProperty =
            AvaloniaProperty.Register<BaseConnection, Size>(nameof(TargetOffset));

        /// <summary>
        /// Defines the <see cref="OffsetModeProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<ConnectionOffsetMode> OffsetModeProperty =
            AvaloniaProperty.Register<BaseConnection, ConnectionOffsetMode>(nameof(SourceOffset));

        /// <summary>
        /// Defines the <see cref="DirectionProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<ConnectionDirection> DirectionProperty =
            AvaloniaProperty.Register<BaseConnection, ConnectionDirection>(nameof(Direction));

        /// <summary>
        /// Defines the <see cref="ArrowHeadEndsProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<ArrowHeadEnds> ArrowHeadEndsProperty =
            AvaloniaProperty.Register<BaseConnection, ArrowHeadEnds>(nameof(ArrowEnds));

        /// <summary>
        /// Defines the <see cref="SpacingProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<double> SpacingProperty =
            AvaloniaProperty.Register<BaseConnection, double>(nameof(Spacing));

        /// <summary>
        /// Defines the <see cref="ArrowSizeProperty"/> property.
        /// </summary>
        public static readonly StyledProperty<Size> ArrowSizeProperty =
            AvaloniaProperty.Register<BaseConnection, Size>(nameof(ArrowSize));

        // avaloniaUI button 참고 하자.
        // ex
        /*
        /// <summary>
        /// Defines the <see cref="Command"/> property.
        /// </summary>
        public static readonly DirectProperty<Button, ICommand?> CommandProperty =
            AvaloniaProperty.RegisterDirect<Button, ICommand?>(nameof(Command),
                button => button.Command, (button, command) => button.Command = command, enableDataValidation: true);
         
         */

        /// <summary>
        /// Defines the <see cref="SplitCommandProperty"/> property.
        /// </summary>
       /* public static readonly StyledProperty<ICommand> SplitCommandProperty =
            AvaloniaProperty.Register<BaseConnection, ICommand>(nameof(SplitCommand));*/

        /// <summary>
        /// Gets or sets the start point of this connection.
        /// </summary>
        public Point Source
        {
            get => (Point)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the end point of this connection.
        /// </summary>
        public Point Target
        {
            get => (Point)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        /// <summary>
        /// Gets or sets the offset from the <see cref="Target"/> point.
        /// </summary>
        public Size TargetOffset
        {
            get => (Size)GetValue(TargetOffsetProperty);
            set => SetValue(TargetOffsetProperty, value);
        }

        /// <summary>
        /// Gets or sets the offset from the <see cref="Source"/> point.
        /// </summary>
        public Size SourceOffset
        {
            get => (Size)GetValue(SourceOffsetProperty);
            set => SetValue(SourceOffsetProperty, value);
        }


        /// <summary>
        /// Gets or sets the <see cref="ConnectionOffsetMode"/> to apply when drawing the connection.
        /// </summary>
        public ConnectionOffsetMode OffsetMode
        {
            get => (ConnectionOffsetMode)GetValue(OffsetModeProperty);
            set => SetValue(OffsetModeProperty, value);
        }


        /// <summary>
        /// Gets or sets the direction in which this connection is oriented.
        /// </summary>
        public ConnectionDirection Direction
        {
            get => (ConnectionDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>
        /// Gets or sets the arrow ends.
        /// </summary>
        public ArrowHeadEnds ArrowEnds
        {
            get => (ArrowHeadEnds)GetValue(ArrowHeadEndsProperty);
            set => SetValue(ArrowHeadEndsProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the arrow head.
        /// </summary>
        public Size ArrowSize
        {
            get => (Size)GetValue(ArrowSizeProperty);
            set => SetValue(ArrowSizeProperty, value);
        }

        /// <summary>
        /// The distance between the start point and the where the angle breaks.
        /// </summary>
        public double Spacing
        {
            get => (double)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        /// <summary>
        /// Splits the connection. Triggered by <see cref="EditorGestures.Connection.Split"/> gesture.
        /// Parameter is the location where the splitting ocurred.
        /// </summary>
      /*  public ICommand? SplitCommand
        {
            get => (ICommand)GetValue(SplitCommandProperty);
            set => SetValue(SplitCommandProperty, value);
        }*/

        #endregion

        /// <summary>
        /// Gets a vector that has its coordinates set to 0.
        /// </summary>
        protected static readonly Vector ZeroVector = new Vector(0d, 0d);

        // TODO 상세하게 살펴보자.
        // Sets path's winding rule (default is EvenOdd). You should call this method before any calls to BeginFigure.
        // If you wonder why, ask Direct2D guys about their design decisions.
        // SetFillRule(FillRule)
        private readonly StreamGeometry _geometry = new StreamGeometry();
       
        // geometry 설정하기.

        protected override Geometry CreateDefiningGeometry()
        {
            using (StreamGeometryContext context = _geometry.Open())
            {
                (Vector sourceOffset, Vector targetOffset) = GetOffset();
                Point source = Source + sourceOffset;
                Point target = Target + targetOffset;

                (Point arrowSource, Point arrowTarget) = DrawLineGeometry(context, source, target);

                if (ArrowSize.Width != 0d && ArrowSize.Height != 0d)
                {
                    switch (ArrowEnds)
                    {
                        case ArrowHeadEnds.Start:
                            DrawArrowGeometry(context, arrowTarget, arrowSource, ConnectionDirection.Backward);
                            break;
                        case ArrowHeadEnds.End:
                            DrawArrowGeometry(context, arrowSource, arrowTarget, ConnectionDirection.Forward);
                            break;
                        case ArrowHeadEnds.Both:
                            DrawArrowGeometry(context, arrowSource, arrowTarget, ConnectionDirection.Forward);
                            DrawArrowGeometry(context, arrowTarget, arrowSource, ConnectionDirection.Backward);
                            break;
                        case ArrowHeadEnds.None:
                            break;
                        default:
                            break;
                    }
                }
            }

            return _geometry;
        }

        /// <summary>
        /// Gets the resulting offset after applying the <see cref="OffsetMode"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual (Vector SourceOffset, Vector TargetOffset) GetOffset()
        {
            Vector delta = Target - Source;
            Vector delta2 = Source - Target;

            return OffsetMode switch
            {
                ConnectionOffsetMode.Rectangle => (GetRectangleModeOffset(delta, SourceOffset), GetRectangleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Circle => (GetCircleModeOffset(delta, SourceOffset), GetCircleModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.Edge => (GetEdgeModeOffset(delta, SourceOffset), GetEdgeModeOffset(delta2, TargetOffset)),
                ConnectionOffsetMode.None => (ZeroVector, ZeroVector),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        // connection 에서 가져옴.
        //protected abstract (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target);


        // ReSharper disable once InconsistentNaming
        private const double _baseOffset = 100d;
        // ReSharper disable once InconsistentNaming
        private const double _offsetGrowthRate = 25d;

        protected (Point ArrowSource, Point ArrowTarget) DrawLineGeometry(StreamGeometryContext context, Point source, Point target)
        {
            double direction = Direction == ConnectionDirection.Forward ? 1d : -1d;
            var spacing = new Vector(Spacing * direction, 0d);
            var arrowOffset = new Vector(ArrowSize.Width * direction, 0d);
            Point endPoint = Spacing > 0 ? target - arrowOffset : target;
            Point startPoint = source + spacing;

            Vector delta = target - source;
            double height = Math.Abs(delta.Y);
            double width = Math.Abs(delta.X);

            // Smooth curve when distance is lower than base offset
            double smooth = Math.Min(_baseOffset, height);
            // Calculate offset based on distance
            double offset = Math.Max(smooth, width / 2d);
            // Grow slowly with distance
            offset = Math.Min(_baseOffset + Math.Sqrt(width * _offsetGrowthRate), offset);

            var controlPoint = new Vector(offset * direction, 0d);

            // TODO 세밀한 부분은 찾아봐야 한다.
            context.BeginFigure(source, false);
            context.LineTo(startPoint);
            // TODO QuadraticBezierTo(Point, Point) 랑 비교해보자.
            context.CubicBezierTo(startPoint + controlPoint, endPoint - controlPoint, endPoint);
            context.LineTo(endPoint);

            return (source, target);
        }

        protected virtual void DrawArrowGeometry(StreamGeometryContext context, Point source, Point target, ConnectionDirection arrowDirection = ConnectionDirection.Forward)
        {
            (Point from, Point to) = GetArrowHeadPoints(source, target, arrowDirection);

            
            // avalonia 에서 찾아보자. 위에랑 비교하고 nodify 랑 비교해야함.
            context.BeginFigure(target, true);
            context.LineTo(from);
            context.LineTo(to);
        }

        protected virtual (Point From, Point To) GetArrowHeadPoints(Point source, Point target, ConnectionDirection arrowDirection)
        {
            double headWidth = ArrowSize.Width;
            double headHeight = ArrowSize.Height;

            double direction = arrowDirection == ConnectionDirection.Forward ? 1d : -1d;
            var from = new Point(target.X - headWidth * direction, target.Y + headHeight);
            var to = new Point(target.X - headWidth * direction, target.Y - headHeight);
            return (from, to);
        }

        private static Vector GetRectangleModeOffset(Vector delta, Size offset)
        {
            if (delta.SquaredLength > 0d)
            {
                delta.Normalize();
            }

            double angle = Math.Atan2(delta.Y, delta.X);

            // 수정 구문 살펴보자.
            // 참고
            // https://sourcegraph.com/github.com/mameolan/Avalonia.ExtendedToolkit/-/blob/Avalonia.Controlz/Controls/TabPanel.cs?L166:20&subtree=true#tab=references
            var result = new Vector();

            if (offset.Width * 2d * Math.Abs(delta.Y) < offset.Height * 2d * Math.Abs(delta.X))
            {
                double X = Math.Sign(delta.X) * offset.Width;
                result = new Vector(X, Math.Tan(angle) * X);
                
            }
            else
            {
                double Y = Math.Sign(delta.Y) * offset.Height;
                double X = 1.0d / Math.Tan(angle) * Y;

                result = new Vector(X, Y);
              
            }

            return result;
        }

        private static Vector GetCircleModeOffset(Vector delta, Size offset)
        {
            if (delta.SquaredLength > 0d)
            {
                delta.Normalize();
            }

            return new Vector(delta.X * offset.Width, delta.Y * offset.Height);
        }

        private static Vector GetEdgeModeOffset(Vector delta, Size offset)
        {
            double xOffset = Math.Min(Math.Abs(delta.X) / 2d, offset.Width) * Math.Sign(delta.X);
            double yOffset = Math.Min(Math.Abs(delta.Y) / 2d, offset.Height) * Math.Sign(delta.Y);

            return new Vector(xOffset, yOffset);
        }
    }
}
