#if WINDOWS
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using BindingMode = Microsoft.Maui.Controls.BindingMode;
using ListViewSelectionMode = Microsoft.UI.Xaml.Controls.ListViewSelectionMode;
using Style = Microsoft.Maui.Controls.Style;

namespace YB.MauiDataGridView;


public partial class MauiDataGrid  : View, IMauiDataGrid, INotifyPropertyChanged
{
    public new event EventHandler<Microsoft.Maui.Controls.PropertyChangingEventArgs>? PropertyChanging;
    public new event EventHandler<EventArgs>? BindingContextChanged;
    public event EventHandler<PropertyChangedEventArgs>? PropertyChangedd;
    public MauiDataGrid()
    {
    }

    //event EventHandler<PropertyChangedEventArgs>? IMauiDataGrid.PropertyChanged;


    // Add Windows-specific events:
    public new event RoutedEventHandler? Loaded;
    public new event RoutedEventHandler? Unloaded;
    public event DoubleTappedEventHandler? DoubleTapped;
    public event PointerEventHandler? PointerEntered;
    public event PointerEventHandler? PointerExited;
    public event PointerEventHandler? PointerMoved;
    public event PointerEventHandler? PointerPressed;
    public event PointerEventHandler? PointerReleased;
    public event PointerEventHandler? PointerWheelChanged;
    public event RightTappedEventHandler? RightTapped;
    //public event EventHandler?<TappedEventArgs> TappedEventArgs;
    public event TappedEventHandler? Tapped;
    public event HoldingEventHandler? Holding;
    public event KeyEventHandler? KeyDown;
    public event KeyEventHandler? KeyUp;
    public event TypedEventHandler<UIElement, CharacterReceivedRoutedEventArgs>? CharacterReceived;
    public event TypedEventHandler<UIElement, RoutedEventArgs>? ContextCanceled;
    public event TypedEventHandler<UIElement, ContextRequestedEventArgs>? ContextRequested;
    public event TypedEventHandler<FrameworkElement, DataContextChangedEventArgs>? DataContextChanged;
    //public event TypedEventHandler<FrameworkElement, PropertyChangedEventArgs>? PropertyChanged;
    public event EventHandler<Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs>? SelectionChanged; // Example - use appropriate EventArgs
    public new event EventHandler<PropertyChangedEventArgs>? PropertyChanged; // Example - use appropriate EventArgs
    
    // Bindable Properties
    public static readonly BindableProperty ColumnsProperty =
        BindableProperty.Create(nameof(Columns), typeof(TableViewColumnsCollection), typeof(MauiDataGrid), null, BindingMode.TwoWay, propertyChanged: OnColumnsChanged);

    // Internal raise methods—must be called from within MyTableView.
    internal void RaiseLoaded(RoutedEventArgs e)
    {
        Loaded?.Invoke(this, e);
    }
    internal void RaiseSelectionChanged(Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
    {
        SelectionChanged?.Invoke(null, e);
    }
    //internal void RaiseProperChanged(PropertyChangedEventArgs e)
    //{
    //    this.PropertyChanged
    //}
    internal void RaiseUnloaded(RoutedEventArgs e) => Unloaded?.Invoke(this, e);
    internal void RaiseDoubleTapped(DoubleTappedRoutedEventArgs e)
    {
        DoubleTapped?.Invoke(this, e);
    }
    internal void RaisePointerEntered(PointerRoutedEventArgs e) => PointerEntered?.Invoke(this, e);
    internal void RaisePointerExited(PointerRoutedEventArgs e) => PointerExited?.Invoke(this, e);
    internal void RaisePointerMoved(PointerRoutedEventArgs e) => PointerMoved?.Invoke(this, e);
    internal void RaisePointerPressed(PointerRoutedEventArgs e) => PointerPressed?.Invoke(this, e);
    internal void RaisePointerReleased(PointerRoutedEventArgs e) => PointerReleased?.Invoke(this, e);
    internal void RaisePointerWheelChanged(PointerRoutedEventArgs e) => PointerWheelChanged?.Invoke(this, e);
    internal void RaiseRightTapped(RightTappedRoutedEventArgs e) => RightTapped?.Invoke(this, e);
    internal void RaiseTapped(TappedRoutedEventArgs e)
    {
        Tapped?.Invoke(this, e);
    }

    internal void RaiseCharacterReceived(CharacterReceivedRoutedEventArgs e) => CharacterReceived?.Invoke(null, e);
    internal void RaiseContextRequested(ContextRequestedEventArgs e) => ContextRequested?.Invoke(null, e);

    internal void RaiseDataContextChanged(object? sender, DataContextChangedEventArgs e) => DataContextChanged?.Invoke(null, e);
    internal void RaisePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(null, e);
    }

    internal void RaiseHolding(HoldingRoutedEventArgs e) => Holding?.Invoke(this, e);
    internal void RaiseKeyDown(KeyRoutedEventArgs e) => KeyDown?.Invoke(this, e);
    internal void RaiseKeyUp(KeyRoutedEventArgs e) => KeyUp?.Invoke(this, e);
    private static void OnColumnsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        //Handle Column Changes.
        if (bindable is MauiDataGrid grid)
        {
            if (oldValue is TableViewColumnsCollection oldColumns)
            {
                oldColumns.CollectionChanged -= grid.Columns_CollectionChanged;
            }
            if (newValue is TableViewColumnsCollection newColumns)
            {
                newColumns.CollectionChanged += grid.Columns_CollectionChanged;
            }
        }
    }
    private void Columns_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        // Handle Column Changes here.
    }



    //internal void RaiseManipulationCompleted(ManipulationCompletedRoutedEventArgs e) => ManipulationCompleted?.Invoke(this, e);
    //internal void RaiseManipulationDelta(ManipulationDeltaRoutedEventArgs e) => ManipulationDelta?.Invoke(this, e);
    //internal void RaiseManipulationInertiaStarting(ManipulationInertiaStartingRoutedEventArgs e) => ManipulationInertiaStarting?.Invoke(this, e);
    //internal void RaiseManipulationStarted(ManipulationStartedRoutedEventArgs e) => ManipulationStarted?.Invoke(this, e);
    //internal void RaiseManipulationStarting(ManipulationStartingRoutedEventArgs e) => ManipulationStarting?.Invoke(this, e);

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
    //public static readonly BindableProperty ColumnsProperty =
    //    BindableProperty.Create(nameof(Columns), typeof(TableViewColumnsCollection), typeof(MauiDataGrid), null);
    public static readonly BindableProperty SelectedItemProperty =
    BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(MauiDataGrid), null, propertyChanged: OnSelectedItemChanged); // 

    private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {

    }

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(MauiDataGrid), null);
    public static readonly BindableProperty AutoGenerateColumnsProperty =
        BindableProperty.Create(nameof(AutoGenerateColumns), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty CanDragItemsProperty =
        BindableProperty.Create(nameof(CanDragItems), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty CanReorderItemsProperty =
        BindableProperty.Create(nameof(CanReorderItems), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty DataFetchSizeProperty =
        BindableProperty.Create(nameof(DataFetchSize), typeof(double), typeof(MauiDataGrid), 0.0);
    public static readonly BindableProperty FooterProperty =
        BindableProperty.Create(nameof(Footer), typeof(object), typeof(MauiDataGrid), null);
    public static readonly BindableProperty FooterTemplateProperty =
        BindableProperty.Create(nameof(FooterTemplate), typeof(Microsoft.Maui.Controls.DataTemplate), typeof(MauiDataGrid), null);
    public static readonly BindableProperty FooterTransitionsProperty =
        BindableProperty.Create(nameof(FooterTransitions), typeof(TransitionCollection), typeof(MauiDataGrid), null);
    public static readonly BindableProperty HeaderProperty =
        BindableProperty.Create(nameof(Header), typeof(object), typeof(MauiDataGrid), null);
    public static readonly BindableProperty HeaderTemplateProperty =
        BindableProperty.Create(nameof(HeaderTemplate), typeof(Microsoft.Maui.Controls.DataTemplate), typeof(MauiDataGrid), null);
    public static readonly BindableProperty HeaderTransitionsProperty =
        BindableProperty.Create(nameof(HeaderTransitions), typeof(TransitionCollection), typeof(MauiDataGrid), null);
    public static readonly BindableProperty IncrementalLoadingThresholdProperty =
        BindableProperty.Create(nameof(IncrementalLoadingThreshold), typeof(double), typeof(MauiDataGrid), 0.0);
    public static readonly BindableProperty IncrementalLoadingTriggerProperty =
        BindableProperty.Create(nameof(IncrementalLoadingTrigger), typeof(IncrementalLoadingTrigger), typeof(MauiDataGrid), IncrementalLoadingTrigger.Edge);
    public static readonly BindableProperty IsActiveViewProperty =
        BindableProperty.Create(nameof(IsActiveView), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty IsItemClickEnabledProperty =
        BindableProperty.Create(nameof(IsItemClickEnabled), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty IsMultiSelectCheckBoxEnabledProperty =
        BindableProperty.Create(nameof(IsMultiSelectCheckBoxEnabled), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty IsSwipeEnabledProperty =
        BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty IsRightClickEnabledProperty =
        BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty IsZoomedInViewProperty =
        BindableProperty.Create(nameof(IsZoomedInView), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty ReorderModeProperty =
        BindableProperty.Create(nameof(ReorderMode), typeof(ListViewReorderMode), typeof(MauiDataGrid), ListViewReorderMode.Disabled);
    public static readonly BindableProperty SelectionModeProperty =
        BindableProperty.Create(nameof(SelectionMode), typeof(ListViewSelectionMode), typeof(MauiDataGrid), ListViewSelectionMode.None);
    public static readonly BindableProperty SemanticZoomOwnerProperty =
        BindableProperty.Create(nameof(SemanticZoomOwner), typeof(object), typeof(MauiDataGrid), null);
    public static readonly BindableProperty ShowsScrollingPlaceholdersProperty =
        BindableProperty.Create(nameof(ShowsScrollingPlaceholders), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty SingleSelectionFollowsFocusProperty =
        BindableProperty.Create(nameof(SingleSelectionFollowsFocus), typeof(bool), typeof(MauiDataGrid), true);


    public static readonly BindableProperty HeaderRowHeightProperty =
          BindableProperty.Create(nameof(HeaderRowHeight), typeof(double), typeof(MauiDataGrid), 32d);
    public static readonly BindableProperty SelectedIndexProperty =
          BindableProperty.Create(nameof(SelectedIndexProperty), typeof(int), typeof(MauiDataGrid), -1);
    public static readonly BindableProperty RowHeightProperty =
        BindableProperty.Create(nameof(RowHeight), typeof(double), typeof(MauiDataGrid), 40d);
    public static readonly BindableProperty RowMaxHeightProperty =
        BindableProperty.Create(nameof(RowMaxHeight), typeof(double), typeof(MauiDataGrid), double.PositiveInfinity);
    public static readonly BindableProperty ShowExportOptionsProperty =
        BindableProperty.Create(nameof(ShowExportOptions), typeof(bool), typeof(MauiDataGrid), false);

    public static readonly BindableProperty IsReadOnlyProperty =
BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(MauiDataGrid), false);
    public static readonly BindableProperty CornerButtonModeProperty =
        BindableProperty.Create(nameof(CornerButtonMode), typeof(TableViewCornerButtonMode), typeof(MauiDataGrid), TableViewCornerButtonMode.Options);
    public static readonly BindableProperty CanResizeColumnsProperty =
        BindableProperty.Create(nameof(CanResizeColumns), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty CanSortColumnsProperty =
        BindableProperty.Create(nameof(CanSortColumns), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty CanFilterColumnsProperty =
        BindableProperty.Create(nameof(CanFilterColumns), typeof(bool), typeof(MauiDataGrid), true);
    public static readonly BindableProperty MinColumnWidthProperty =
        BindableProperty.Create(nameof(MinColumnWidth), typeof(double), typeof(MauiDataGrid), 50d);
    public static readonly BindableProperty MaxColumnWidthProperty =
        BindableProperty.Create(nameof(MaxColumnWidth), typeof(double), typeof(MauiDataGrid), double.PositiveInfinity);
    public static readonly BindableProperty SelectionUnitProperty =
        BindableProperty.Create(nameof(SelectionUnit), typeof(TableViewSelectionUnit), typeof(MauiDataGrid), TableViewSelectionUnit.CellOrRow);
    public static readonly BindableProperty HeaderGridLinesVisibilityProperty =
        BindableProperty.Create(nameof(HeaderGridLinesVisibility), typeof(TableViewGridLinesVisibility), typeof(MauiDataGrid), TableViewGridLinesVisibility.All);
    public static readonly BindableProperty GridLinesVisibilityProperty =
        BindableProperty.Create(nameof(GridLinesVisibility), typeof(TableViewGridLinesVisibility), typeof(MauiDataGrid), TableViewGridLinesVisibility.All);
    public static readonly BindableProperty HorizontalGridLinesStrokeThicknessProperty =
        BindableProperty.Create(nameof(HorizontalGridLinesStrokeThickness), typeof(double), typeof(MauiDataGrid), 1d);
    public static readonly BindableProperty VerticalGridLinesStrokeThicknessProperty =
        BindableProperty.Create(nameof(VerticalGridLinesStrokeThickness), typeof(double), typeof(MauiDataGrid), 1d);
    public static readonly BindableProperty HorizontalGridLinesStrokeProperty =
        BindableProperty.Create(nameof(HorizontalGridLinesStroke), typeof(Brush), typeof(MauiDataGrid), default(Brush));
    public static readonly BindableProperty VerticalGridLinesStrokeProperty =
        BindableProperty.Create(nameof(VerticalGridLinesStroke), typeof(Brush), typeof(MauiDataGrid), default(Brush));
    public static readonly BindableProperty AlternateRowForegroundProperty =
        BindableProperty.Create(nameof(AlternateRowForeground), typeof(Brush), typeof(MauiDataGrid), null);
    public static readonly BindableProperty AlternateRowBackgroundProperty =
        BindableProperty.Create(nameof(AlternateRowBackground), typeof(Brush), typeof(MauiDataGrid), null);
    public static readonly BindableProperty RowContextFlyoutProperty =
        BindableProperty.Create(nameof(RowContextFlyout), typeof(FlyoutBase), typeof(MauiDataGrid), null);
    public static readonly BindableProperty CellContextFlyoutProperty =
        BindableProperty.Create(nameof(CellContextFlyout), typeof(FlyoutBase), typeof(MauiDataGrid), null);
    public static readonly BindableProperty ColumnHeaderStyleProperty =
        BindableProperty.Create(nameof(ColumnHeaderStyle), typeof(Style), typeof(MauiDataGrid), null);
    public static readonly BindableProperty CellStyleProperty =
        BindableProperty.Create(nameof(CellStyle), typeof(Style), typeof(MauiDataGrid), null);

    public double HeaderRowHeight { get => (double)GetValue(HeaderRowHeightProperty); set => SetValue(HeaderRowHeightProperty, value); }
    public double RowHeight { get => (double)GetValue(RowHeightProperty); set => SetValue(RowHeightProperty, value); }
    public double RowMaxHeight { get => (double)GetValue(RowMaxHeightProperty); set => SetValue(RowMaxHeightProperty, value); }
    public bool ShowExportOptions { get => (bool)GetValue(ShowExportOptionsProperty); set => SetValue(ShowExportOptionsProperty, value); }


    public object ItemsSource { get => GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }
    public bool AutoGenerateColumns { get => (bool)GetValue(AutoGenerateColumnsProperty); set => SetValue(AutoGenerateColumnsProperty, value); }
    public bool CanDragItems { get => (bool)GetValue(CanDragItemsProperty); set => SetValue(CanDragItemsProperty, value); }
    public bool CanReorderItems { get => (bool)GetValue(CanReorderItemsProperty); set => SetValue(CanReorderItemsProperty, value); }
    public double DataFetchSize { get => (double)GetValue(DataFetchSizeProperty); set => SetValue(DataFetchSizeProperty, value); }
    public object Footer { get => GetValue(FooterProperty); set => SetValue(FooterProperty, value); }
    public Microsoft.Maui.Controls.DataTemplate FooterTemplate { get => (Microsoft.Maui.Controls.DataTemplate)GetValue(FooterTemplateProperty); set => SetValue(FooterTemplateProperty, value); }
    public TransitionCollection FooterTransitions { get => (TransitionCollection)GetValue(FooterTransitionsProperty); set => SetValue(FooterTransitionsProperty, value); }
    public object Header { get => GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    public Microsoft.Maui.Controls.DataTemplate HeaderTemplate { get => (Microsoft.Maui.Controls.DataTemplate)GetValue(HeaderTemplateProperty); set => SetValue(HeaderTemplateProperty, value); }
    public TransitionCollection HeaderTransitions { get => (TransitionCollection)GetValue(HeaderTransitionsProperty); set => SetValue(HeaderTransitionsProperty, value); }

    public TableViewColumnsCollection Columns { get => (TableViewColumnsCollection)GetValue(ColumnsProperty); set => SetValue(ColumnsProperty, value); }

    public double IncrementalLoadingThreshold { get => (double)GetValue(IncrementalLoadingThresholdProperty); set => SetValue(IncrementalLoadingThresholdProperty, value); }
    public IncrementalLoadingTrigger IncrementalLoadingTrigger { get => (IncrementalLoadingTrigger)GetValue(IncrementalLoadingTriggerProperty); set => SetValue(IncrementalLoadingTriggerProperty, value); }
    public bool IsActiveView { get => (bool)GetValue(IsActiveViewProperty); set => SetValue(IsActiveViewProperty, value); }
    public bool IsItemClickEnabled { get => (bool)GetValue(IsItemClickEnabledProperty); set => SetValue(IsItemClickEnabledProperty, value); }
    public bool IsMultiSelectCheckBoxEnabled { get => (bool)GetValue(IsMultiSelectCheckBoxEnabledProperty); set => SetValue(IsMultiSelectCheckBoxEnabledProperty, value); }
    public bool IsSwipeEnabled { get => (bool)GetValue(IsSwipeEnabledProperty); set => SetValue(IsSwipeEnabledProperty, value); }
    public bool IsRightClickEnabled { get => (bool)GetValue(IsRightClickEnabledProperty); set => SetValue(IsRightClickEnabledProperty, value); }
    public bool IsZoomedInView { get => (bool)GetValue(IsZoomedInViewProperty); set => SetValue(IsZoomedInViewProperty, value); }
    public ListViewReorderMode ReorderMode { get => (ListViewReorderMode)GetValue(ReorderModeProperty); set => SetValue(ReorderModeProperty, value); }
    public ListViewSelectionMode SelectionMode { get => (ListViewSelectionMode)GetValue(SelectionModeProperty); set => SetValue(SelectionModeProperty, value); }
    public object SemanticZoomOwner { get => GetValue(SemanticZoomOwnerProperty); set => SetValue(SemanticZoomOwnerProperty, value); }
    public bool ShowsScrollingPlaceholders { get => (bool)GetValue(ShowsScrollingPlaceholdersProperty); set => SetValue(ShowsScrollingPlaceholdersProperty, value); }
    public bool SingleSelectionFollowsFocus { get => (bool)GetValue(SingleSelectionFollowsFocusProperty); set => SetValue(SingleSelectionFollowsFocusProperty, value); }
    public bool IsReadOnly { get => (bool)GetValue(IsReadOnlyProperty); set => SetValue(IsReadOnlyProperty, value); }
    public TableViewCornerButtonMode CornerButtonMode { get => (TableViewCornerButtonMode)GetValue(CornerButtonModeProperty); set => SetValue(CornerButtonModeProperty, value); }
    public bool CanResizeColumns { get => (bool)GetValue(CanResizeColumnsProperty); set => SetValue(CanResizeColumnsProperty, value); }
    public bool CanSortColumns { get => (bool)GetValue(CanSortColumnsProperty); set => SetValue(CanSortColumnsProperty, value); }
    public bool CanFilterColumns { get => (bool)GetValue(CanFilterColumnsProperty); set => SetValue(CanFilterColumnsProperty, value); }
    public double MinColumnWidth { get => (double)GetValue(MinColumnWidthProperty); set => SetValue(MinColumnWidthProperty, value); }
    public double MaxColumnWidth { get => (double)GetValue(MaxColumnWidthProperty); set => SetValue(MaxColumnWidthProperty, value); }
    public TableViewSelectionUnit SelectionUnit { get => (TableViewSelectionUnit)GetValue(SelectionUnitProperty); set => SetValue(SelectionUnitProperty, value); }
    public TableViewGridLinesVisibility HeaderGridLinesVisibility { get => (TableViewGridLinesVisibility)GetValue(HeaderGridLinesVisibilityProperty); set => SetValue(HeaderGridLinesVisibilityProperty, value); }
    public TableViewGridLinesVisibility GridLinesVisibility { get => (TableViewGridLinesVisibility)GetValue(GridLinesVisibilityProperty); set => SetValue(GridLinesVisibilityProperty, value); }
    public double HorizontalGridLinesStrokeThickness { get => (double)GetValue(HorizontalGridLinesStrokeThicknessProperty); set => SetValue(HorizontalGridLinesStrokeThicknessProperty, value); }
    public double VerticalGridLinesStrokeThickness { get => (double)GetValue(VerticalGridLinesStrokeThicknessProperty); set => SetValue(VerticalGridLinesStrokeThicknessProperty, value); }
    public Brush HorizontalGridLinesStroke { get => (Brush)GetValue(HorizontalGridLinesStrokeProperty); set => SetValue(HorizontalGridLinesStrokeProperty, value); }
    public Brush VerticalGridLinesStroke { get => (Brush)GetValue(VerticalGridLinesStrokeProperty); set => SetValue(VerticalGridLinesStrokeProperty, value); }
    public Brush AlternateRowForeground { get => (Brush)GetValue(AlternateRowForegroundProperty); set => SetValue(AlternateRowForegroundProperty, value); }
    public Brush AlternateRowBackground { get => (Brush)GetValue(AlternateRowBackgroundProperty); set => SetValue(AlternateRowBackgroundProperty, value); }
    public FlyoutBase RowContextFlyout { get => (FlyoutBase)GetValue(RowContextFlyoutProperty); set => SetValue(RowContextFlyoutProperty, value); }
    public FlyoutBase CellContextFlyout { get => (FlyoutBase)GetValue(CellContextFlyoutProperty); set => SetValue(CellContextFlyoutProperty, value); }
    public Style ColumnHeaderStyle { get => (Style)GetValue(ColumnHeaderStyleProperty); set => SetValue(ColumnHeaderStyleProperty, value); }
  
    public Style CellStyle { get => (Style)GetValue(CellStyleProperty); set => SetValue(CellStyleProperty, value); }
    
    public object SelectedValue { get => (object)GetValue(SelectedItemProperty); set => SetValue(SelectedItemProperty, value); }
    public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }
    //public ListViewSelectionMode SelectionMode { get => (ListViewSelectionMode)GetValue(SelectionModeProperty); set => SetValue(SelectionModeProperty, value); }
    public object SelectedValuePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    
    public ICollectionView CollectionView => throw new NotImplementedException();

    public IList<SortDescription> SortDescriptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IList<FilterDescription> GroupDescriptions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    
    Microsoft.UI.Xaml.Style IMauiDataGrid.CellStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    Microsoft.UI.Xaml.Style IMauiDataGrid.ColumnHeaderStyle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void ScrollIntoView(object item)
    {
        // Implementation in the handler
    }


    //internal void PropertyChangedd(PropertyChangedEventHandler e)
    //{

    //}
}
#endif


