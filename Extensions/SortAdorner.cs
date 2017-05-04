﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Extensions
{
    public class SortAdorner : Adorner
    {
        private static readonly Geometry AscGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static readonly Geometry DescGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; }

        public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
        {
            Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                    (
                            AdornedElement.RenderSize.Width - 15,
                            (AdornedElement.RenderSize.Height - 5) / 2
                    );
            drawingContext.PushTransform(transform);

            Geometry geometry = AscGeometry;
            if (Direction == ListSortDirection.Descending)
                geometry = DescGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}