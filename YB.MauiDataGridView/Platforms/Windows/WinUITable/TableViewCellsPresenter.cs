﻿using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Colors = Microsoft.UI.Colors;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;
using Visibility = Microsoft.UI.Xaml.Visibility;

namespace YB.MauiDataGridView;

/// <summary>
/// Represents a presenter for the cells in a TableView row.
/// </summary>
public partial class TableViewCellsPresenter : Control
{
    private StackPanel? _stackPanel;
    private Rectangle? _v_gridLine;
    private Rectangle? _h_gridLine;

    /// <summary>
    /// Initializes a new instance of the TableViewCellsPresenter class.
    /// </summary>
    public TableViewCellsPresenter()
    {
        DefaultStyleKey = typeof(TableViewCellsPresenter);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _stackPanel = GetTemplateChild("StackPanel") as StackPanel;
        _v_gridLine = GetTemplateChild("VerticalGridLine") as Rectangle;
        _h_gridLine = GetTemplateChild("HorizontalGridLine") as Rectangle;

        TableViewRow = this.FindAscendant<TableViewRow>();
        TableView = TableViewRow?.TableView;

        EnsureGridLines();
    }

    /// <summary>
    /// Ensures grid lines are applied to the cells.
    /// </summary>
    internal void EnsureGridLines()
    {
        if (TableView is null) return;

        if (_h_gridLine is not null)
        {
            _h_gridLine.Fill = TableView.HorizontalGridLinesStroke;
            _h_gridLine.Height = TableView.HorizontalGridLinesStrokeThickness;
            _h_gridLine.Visibility = TableView.GridLinesVisibility is TableViewGridLinesVisibility.All or TableViewGridLinesVisibility.Horizontal
                                     ? Visibility.Visible : Visibility.Collapsed;

            if (_v_gridLine is not null)
            {
                _v_gridLine.Fill = TableView.HeaderGridLinesVisibility is TableViewGridLinesVisibility.All or TableViewGridLinesVisibility.Vertical
                                   ? TableView.VerticalGridLinesStroke : new SolidColorBrush(Colors.Transparent);
                _v_gridLine.Width = TableView.VerticalGridLinesStrokeThickness;
                _v_gridLine.Visibility = TableView.HeaderGridLinesVisibility is TableViewGridLinesVisibility.All or TableViewGridLinesVisibility.Vertical
                                         || TableView.GridLinesVisibility is TableViewGridLinesVisibility.All or TableViewGridLinesVisibility.Vertical
                                         ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        foreach (var cell in Cells)
        {
            cell.EnsureGridLines();
        }
    }

    /// <summary>
    /// Gets the collection of child elements.
    /// </summary>
    internal UIElementCollection Children => _stackPanel?.Children!;

    /// <summary>
    /// Gets the list of cells in the presenter.
    /// </summary>
    public IList<TableViewCell> Cells => _stackPanel?.Children.OfType<TableViewCell>().ToList()!;

    /// <summary>
    /// Gets or sets the TableViewRow associated with the presenter.
    /// </summary>
    public TableViewRow? TableViewRow { get; private set; }

    /// <summary>
    /// Gets or sets the TableView associated with the presenter.
    /// </summary>
    public TableView? TableView { get; private set; }
}
