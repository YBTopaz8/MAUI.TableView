#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Foundation;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataTemplate = Microsoft.UI.Xaml.DataTemplate;
using ListViewSelectionMode = Microsoft.UI.Xaml.Controls.ListViewSelectionMode;
using Style = Microsoft.UI.Xaml.Style;
using TableViewColumnsCollection = YB.MauiDataGridView.TableViewColumnsCollection;
using Microsoft.UI.Xaml.Controls.Primitives;
using FlyoutBase = Microsoft.Maui.Controls.FlyoutBase;
using YB.MauiDataGridView;
using SortDescription = YB.MauiDataGridView.SortDescription;
using FilterDescription = YB.MauiDataGridView.FilterDescription;
#endif
namespace YB.MauiDataGridView;

public interface IMauiDataGrid
{

    // Core properties
    object ItemsSource { get; set; }
    bool AutoGenerateColumns { get; set; }
    bool CanDragItems { get; set; }
    bool CanReorderItems { get; set; }
    double DataFetchSize { get; set; }
    object Footer { get; set; }
    Microsoft.Maui.Controls.DataTemplate FooterTemplate { get; set; }
#if WINDOWS
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
    event EventHandler<PropertyChangedEventArgs>? PropertyChanged;

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
    IList<SortDescription> SortDescriptions { get; set; }
    IList<FilterDescription> GroupDescriptions { get; set; }

    // Add a Columns collection for XAML support
    TableViewColumnsCollection Columns { get; }
    //Selected Item
    object SelectedItem { get; set; }
    object SelectedValue { get; set; }
    object SelectedValuePath { get; set; }
    int SelectedIndex { get; set; }

    void ScrollIntoView(object item);
#endif
}




public static class DataGridHandler
{
    public static void ConfigureTableViewHandler(IMauiHandlersCollection handlers)
    {
#if WINDOWS
        handlers.AddHandler<MauiDataGrid, MauiDataGridHandler>();
#elif ANDROID
        throw new NotImplementedException();
#elif IOS
        throw new NotImplementedException();
#elif MACCATALYST
        throw new NotImplementedException();

#endif
    }
}