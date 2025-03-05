#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;
using WinUI.TableView;
using TableView = global::WinUI.TableView;
using DataTemplate = Microsoft.UI.Xaml.DataTemplate;
using ListViewSelectionMode = Microsoft.UI.Xaml.Controls.ListViewSelectionMode;
using Style = Microsoft.UI.Xaml.Style;
using TableViewColumnsCollection = YB.MAUITableView.Platforms.Windows.WinUITable.TableViewColumnsCollection;
using Microsoft.UI.Xaml.Controls.Primitives;
using FlyoutBase = Microsoft.Maui.Controls.FlyoutBase;
using System.ComponentModel;
using Microsoft.UI.Xaml.Data;
using YB.MAUITableView;
using YB.MAUITableView.Platforms.Windows.WinUITable.ItemsSource;
using YB.MAUITableView.Platforms.Windows.WinUITable;
using TableViewSelectionUnit = WinUI.TableView.TableViewSelectionUnit;
using Microsoft.Maui.Controls;


namespace MAUI.Windows.TableView;

public interface ITableView
{
    // Core properties
    object ItemsSource { get; set; }
    bool AutoGenerateColumns { get; set; }
    bool CanDragItems { get; set; }
    bool CanReorderItems { get; set; }
    double DataFetchSize { get; set; }
    object Footer { get; set; }
    Microsoft.Maui.Controls.DataTemplate FooterTemplate { get; set; }
    TransitionCollection FooterTransitions { get; set; }
    object Header { get; set; }
    Microsoft.Maui.Controls.DataTemplate HeaderTemplate { get; set; }
    TransitionCollection HeaderTransitions { get; set; }
    double IncrementalLoadingThreshold { get; set; }
    IncrementalLoadingTrigger IncrementalLoadingTrigger { get; set; }
    bool IsActiveView { get; set; }
    bool IsItemClickEnabled { get; set; }
    bool IsMultiSelectCheckBoxEnabled { get; set; }
    bool IsSwipeEnabled { get; set; }
    bool IsZoomedInView { get; set; }
    ListViewReorderMode ReorderMode { get; set; }
    ListViewSelectionMode SelectionMode { get; set; }
    object SemanticZoomOwner { get; set; }
    bool ShowsScrollingPlaceholders { get; set; }
    bool SingleSelectionFollowsFocus { get; set; }


    // Interaction events
    event RoutedEventHandler Loaded;
    event RoutedEventHandler Unloaded;
    event DoubleTappedEventHandler DoubleTapped;
    event PointerEventHandler PointerEntered;
    event PointerEventHandler PointerExited;
    event PointerEventHandler PointerMoved;
    event PointerEventHandler PointerPressed;
    event PointerEventHandler PointerReleased;
    event PointerEventHandler PointerWheelChanged;
    event RightTappedEventHandler RightTapped;
    event TappedEventHandler Tapped;
    event HoldingEventHandler Holding;
    event KeyEventHandler KeyDown;
    event KeyEventHandler KeyUp;
    event TypedEventHandler<UIElement, CharacterReceivedRoutedEventArgs> CharacterReceived;
    event TypedEventHandler<UIElement, ContextRequestedEventArgs> ContextRequested;
    event TypedEventHandler<FrameworkElement, DataContextChangedEventArgs> DataContextChanged;
    event TypedEventHandler<FrameworkElement, PropertyChangedEventArgs>? PropertyChanged;

    // Native appearance and behavior properties
    double HeaderRowHeight { get; set; }
    double RowHeight { get; set; }
    double RowMaxHeight { get; set; }
    bool ShowExportOptions { get; set; }
    bool IsReadOnly { get; set; }
    TableViewCornerButtonMode CornerButtonMode { get; set; }
    bool CanResizeColumns { get; set; }
    bool CanSortColumns { get; set; }
    bool CanFilterColumns { get; set; }
    double MinColumnWidth { get; set; }
    double MaxColumnWidth { get; set; }
    TableViewSelectionUnit SelectionUnit { get; set; }
    TableViewGridLinesVisibility HeaderGridLinesVisibility { get; set; }
    TableViewGridLinesVisibility GridLinesVisibility { get; set; }
    double HorizontalGridLinesStrokeThickness { get; set; }
    double VerticalGridLinesStrokeThickness { get; set; }
    Brush HorizontalGridLinesStroke { get; set; }
    Brush VerticalGridLinesStroke { get; set; }
    Brush AlternateRowForeground { get; set; }
    Brush AlternateRowBackground { get; set; }
    FlyoutBase RowContextFlyout { get; set; }
    FlyoutBase CellContextFlyout { get; set; }
    Style ColumnHeaderStyle { get; set; }
    Style CellStyle { get; set; }
    ICollectionView CollectionView { get; }
    IList<ListSortDescription> SortDescriptions { get; set; }
    IList<FilterDescription> GroupDescriptions { get; set; }

    // Add a Columns collection for XAML support
    TableViewColumnsCollection Columns { get; }
    //Selected Item
    object SelectedItem { get; set; }
    object SelectedValue { get; set; }
    object SelectedValuePath { get; set; }
    object SelectedIndex { get; set; }

    void ScrollIntoView(object item);
}


/// <summary>
/// Specifies the mode of the corner button in a TableView.
/// </summary>
public enum TableViewCornerButtonMode
{
    /// <summary>
    /// No button.
    /// </summary>
    None,

    /// <summary>
    /// Show Select All button.
    /// </summary>
    SelectAll,

    /// <summary>
    /// Show Options button.
    /// </summary>
    Options
}

/// <summary>
/// Specifies the visibility of grid lines in a TableView.
/// </summary>
public enum TableViewGridLinesVisibility
{
    /// <summary>
    /// Both horizontal and vertical grid lines are visible.
    /// </summary>
    All,

    /// <summary>
    /// Only horizontal grid lines are visible.
    /// </summary>
    Horizontal,

    /// <summary>
    /// No grid lines are visible.
    /// </summary>
    None,

    /// <summary>
    /// Only vertical grid lines are visible.
    /// </summary>
    Vertical
}
#endif