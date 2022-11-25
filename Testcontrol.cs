using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalebUI
{
    /// <summary>
    /// 커스텀 도형
    /// </summary>
    public class CustomShape : Shape
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Property
        ////////////////////////////////////////////////////////////////////////////////////////// Protected

        #region 정의 지오메트리 - DefiningGeometry

        protected override Geometry? CreateDefiningGeometry()
        {
            return GetGeometry();
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Private

        #region 지오메트리 구하기 - GetGeometry()

        /// <summary>
        /// 지오메트리 구하기
        /// </summary>
        /// <returns>지오메트리</returns>
        private Geometry GetGeometry()
        {
            StreamGeometry streamGeometry = new StreamGeometry();

            using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
            {
                streamGeometryContext.BeginFigure
                (
                    new Point(50.0, 50.0),
                    false
                );

                streamGeometryContext.ArcTo
                (
                    new Point(75.0, 75.0),
                    new Size(10.0, 20.0),
                    0.0,
                    false,
                    SweepDirection.Clockwise
                );

                streamGeometryContext.ArcTo
                (
                    new Point(100.0, 100.0),
                    new Size(10.0, 20.0),
                    0.0,
                    false,
                    SweepDirection.Clockwise
                );
            }

            return streamGeometry;
        }

        #endregion
    }
}
