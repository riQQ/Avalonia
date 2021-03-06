// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Xunit;

namespace Avalonia.Controls.UnitTests
{
    public class ScrollViewerTests
    {
        [Fact]
        public void Content_Is_Created()
        {
            var target = new ScrollViewer
            {
                Template = new FuncControlTemplate<ScrollViewer>(CreateTemplate),
                Content = "Foo",
            };

            target.ApplyTemplate();
            ((ContentPresenter)target.Presenter).UpdateChild();

            Assert.IsType<TextBlock>(target.Presenter.Child);
        }

        [Fact]
        public void Offset_Should_Be_Coerced_To_Viewport()
        {
            var target = new ScrollViewer();
            target.SetValue(ScrollViewer.ExtentProperty, new Size(20, 20));
            target.SetValue(ScrollViewer.ViewportProperty, new Size(10, 10));
            target.Offset = new Vector(12, 12);

            Assert.Equal(new Vector(10, 10), target.Offset);
        }

        private Control CreateTemplate(ScrollViewer control)
        {
            return new Grid
            {
                ColumnDefinitions = new ColumnDefinitions
                {
                    new ColumnDefinition(1, GridUnitType.Star),
                    new ColumnDefinition(GridLength.Auto),
                },
                RowDefinitions = new RowDefinitions
                {
                    new RowDefinition(1, GridUnitType.Star),
                    new RowDefinition(GridLength.Auto),
                },
                Children =
                {
                    new ScrollContentPresenter
                    {
                        Name = "PART_ContentPresenter",
                        [~ContentPresenter.ContentProperty] = control[~ContentControl.ContentProperty],
                        [~~ScrollContentPresenter.ExtentProperty] = control[~~ScrollViewer.ExtentProperty],
                        [~~ScrollContentPresenter.OffsetProperty] = control[~~ScrollViewer.OffsetProperty],
                        [~~ScrollContentPresenter.ViewportProperty] = control[~~ScrollViewer.ViewportProperty],
                        [~ScrollContentPresenter.CanScrollHorizontallyProperty] = control[~ScrollViewer.CanScrollHorizontallyProperty],
                    },
                    new ScrollBar
                    {
                        Name = "horizontalScrollBar",
                        Orientation = Orientation.Horizontal,
                        [~RangeBase.MaximumProperty] = control[~ScrollViewer.HorizontalScrollBarMaximumProperty],
                        [~~RangeBase.ValueProperty] = control[~~ScrollViewer.HorizontalScrollBarValueProperty],
                        [~ScrollBar.ViewportSizeProperty] = control[~ScrollViewer.HorizontalScrollBarViewportSizeProperty],
                        [~ScrollBar.VisibilityProperty] = control[~ScrollViewer.HorizontalScrollBarVisibilityProperty],
                        [Grid.RowProperty] = 1,
                    },
                    new ScrollBar
                    {
                        Name = "verticalScrollBar",
                        Orientation = Orientation.Vertical,
                        [~RangeBase.MaximumProperty] = control[~ScrollViewer.VerticalScrollBarMaximumProperty],
                        [~~RangeBase.ValueProperty] = control[~~ScrollViewer.VerticalScrollBarValueProperty],
                        [~ScrollBar.ViewportSizeProperty] = control[~ScrollViewer.VerticalScrollBarViewportSizeProperty],
                        [~ScrollBar.VisibilityProperty] = control[~ScrollViewer.VerticalScrollBarVisibilityProperty],
                        [Grid.ColumnProperty] = 1,
                    },
                },
            };
        }
    }
}