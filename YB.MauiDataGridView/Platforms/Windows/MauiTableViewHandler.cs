
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using WinUIGrid = YB.MauiDataGridView.TableView;

using Microsoft.Maui.Handlers;
using ListViewSelectionMode = Microsoft.UI.Xaml.Controls.ListViewSelectionMode;
using Microsoft.UI.Xaml.Media.Animation;
using DataTemplate = Microsoft.UI.Xaml.DataTemplate;

using Microsoft.UI.Xaml.Input;

using GridLength = Microsoft.UI.Xaml.GridLength;
using GridUnitType = Microsoft.UI.Xaml.GridUnitType;
using SelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs;
using YB.MauiDataGridView;
using TableView = YB.MauiDataGridView.TableView;
using System.ComponentModel;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using YB.MauiDataGridView.Platforms.Windows.WinUITable;
using System.Diagnostics;

namespace YB.MauiDataGridView;

// All the code in this file is only included on Windows.

public partial class TableViewImplementation : WinUIGrid, INotifyPropertyChanged
{
    private TableView _tableView;
    private bool _autoGenerateColumns;

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    public TableViewImplementation()
    {
        _tableView = new TableView();

        AutoGenerateColumns = true;

        //_tableView.Background = new Microsoft.UI.Xaml.Media.Brush //need brush to be #191719

        ItemsSource = null;
        //_tableView
        //Children.Add(_tableView);

        try
        {
            this.Loaded += TableViewImplementation_Loaded;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error initializing TableView: {ex.Message}");
        }

     
    }

    private void TableViewImplementation_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateTableView();
    }
    #region Declare Props

    public object? ItemsSource
    {
        get => _tableView.ItemsSource;
        set
        {
            _tableView.ItemsSource = (System.Collections.IList?)value;
            OnPropertyChanged(nameof(ItemsSource));
            UpdateTableView();
        }
    }

    public bool AutoGenerateColumns
    {
        get => _autoGenerateColumns;
        set
        {
            _autoGenerateColumns = value;
            _tableView.AutoGenerateColumns = value;
            OnPropertyChanged(nameof(AutoGenerateColumns));
            UpdateTableView();
            
        }
    }

    // --- Exposed native properties ---
    public bool CanDragItems
    {
        get => _tableView.CanDragItems;
        set { _tableView.CanDragItems = value; OnPropertyChanged(nameof(CanDragItems)); }
    }

    public bool CanReorderItems
    {
        get => _tableView.CanReorderItems;
        set { _tableView.CanReorderItems = value; OnPropertyChanged(nameof(CanReorderItems)); }
    }

    public double DataFetchSize
    {
        get => _tableView.DataFetchSize;
        set { _tableView.DataFetchSize = value; OnPropertyChanged(nameof(DataFetchSize)); }
    }

    public object Footer
    {
        get => _tableView.Footer;
        set { _tableView.Footer = value; OnPropertyChanged(nameof(Footer)); }
    }

    public DataTemplate FooterTemplate
    {
        get => _tableView.FooterTemplate;
        set { _tableView.FooterTemplate = value; OnPropertyChanged(nameof(FooterTemplate)); }
    }

    public TransitionCollection FooterTransitions
    {
        get => _tableView.FooterTransitions;
        set { _tableView.FooterTransitions = value; OnPropertyChanged(nameof(FooterTransitions)); }
    }

    public object Header
    {
        get => _tableView.Header;
        set { _tableView.Header = value; OnPropertyChanged(nameof(Header)); }
    }

    public DataTemplate HeaderTemplate
    {
        get => _tableView.HeaderTemplate;
        set { _tableView.HeaderTemplate = value; OnPropertyChanged(nameof(HeaderTemplate)); }
    }

    public TransitionCollection HeaderTransitions
    {
        get => _tableView.HeaderTransitions;
        set { _tableView.HeaderTransitions = value; OnPropertyChanged(nameof(HeaderTransitions)); }
    }



    public double IncrementalLoadingThreshold
    {

        get => _tableView.IncrementalLoadingThreshold;
        set { _tableView.IncrementalLoadingThreshold = value; OnPropertyChanged(nameof(IncrementalLoadingThreshold)); }
    }

    public IncrementalLoadingTrigger IncrementalLoadingTrigger
    {
        get => _tableView.IncrementalLoadingTrigger;
        set { _tableView.IncrementalLoadingTrigger = value; OnPropertyChanged(nameof(IncrementalLoadingTrigger)); }
    }

    public bool IsReadOnly
    {
        get => _tableView.IsReadOnly;
        set { _tableView.IsReadOnly = value; OnPropertyChanged(nameof(IsReadOnly)); }

    }

    // Selected index
    public object SelectedItem
    {
        get => _tableView.SelectedItem;
        set { _tableView.SelectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
    }
    public object SelectedValue
    {
        get => _tableView.SelectedValue;
        set { _tableView.SelectedValue = value; OnPropertyChanged(nameof(SelectedValue)); }
    }
    public IList<object> SelectedItems
    {
        get => _tableView.SelectedItems;
        set { _tableView.SelectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
    }
    public int SelectedIndex
    {
        get => _tableView.SelectedIndex;
        set { _tableView.SelectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); }
    }
    public bool IsActiveView
    {
        get => _tableView.IsActiveView;
        set { _tableView.IsActiveView = value; OnPropertyChanged(nameof(IsActiveView)); }
    }

    public bool IsItemClickEnabled
    {
        get => _tableView.IsItemClickEnabled;
        set { _tableView.IsItemClickEnabled = value; OnPropertyChanged(nameof(IsItemClickEnabled)); }
    }

    public bool IsMultiSelectCheckBoxEnabled
    {
        get => _tableView.IsMultiSelectCheckBoxEnabled;
        set { _tableView.IsMultiSelectCheckBoxEnabled = value; OnPropertyChanged(nameof(IsMultiSelectCheckBoxEnabled)); }
    }

    public bool IsSwipeEnabled
    {
        get => _tableView.IsSwipeEnabled;
        set { _tableView.IsSwipeEnabled = value; OnPropertyChanged(nameof(IsSwipeEnabled)); }
    }


    public bool IsZoomedInView
    {
        get => _tableView.IsZoomedInView;
        set { _tableView.IsZoomedInView = value; OnPropertyChanged(nameof(IsZoomedInView)); }
    }

    public ListViewReorderMode ReorderMode
    {
        get => _tableView.ReorderMode;
        set { _tableView.ReorderMode = value; OnPropertyChanged(nameof(ReorderMode)); }
    }

    public ListViewSelectionMode SelectionMode
    {
        get => _tableView.SelectionMode;
        set { _tableView.SelectionMode = value; OnPropertyChanged(nameof(SelectionMode)); }
    }


    public bool ShowsScrollingPlaceholders
    {
        get => _tableView.ShowsScrollingPlaceholders;
        set { _tableView.ShowsScrollingPlaceholders = value; OnPropertyChanged(nameof(ShowsScrollingPlaceholders)); }
    }

    public bool SingleSelectionFollowsFocus
    {
        get => _tableView.SingleSelectionFollowsFocus;
        set { _tableView.SingleSelectionFollowsFocus = value; OnPropertyChanged(nameof(SingleSelectionFollowsFocus)); }
    }

    #endregion

    internal readonly record struct TableViewCellSlot(int Row, int Column);

    //public IAdvancedCollectionView CollectionView { get; private set; } = new AdvancedCollectionView();
    internal IDictionary<string, Predicate<object>> ActiveFilters { get; } = new Dictionary<string, Predicate<object>>();
    internal TableViewSelectionUnit LastSelectionUnit { get; set; }
    internal TableViewCellSlot? CurrentCellSlot { get; set; }
    internal TableViewCellSlot? SelectionStartCellSlot { get; set; }
    internal int? SelectionStartRowIndex { get; set; }
    internal HashSet<TableViewCellSlot> SelectedCells { get; set; } = new HashSet<TableViewCellSlot>();
    internal HashSet<HashSet<TableViewCellSlot>> SelectedCellRanges { get; } = new HashSet<HashSet<TableViewCellSlot>>();
    internal bool IsEditing { get; set; }
    internal int SelectionIndicatorWidth => SelectionMode is ListViewSelectionMode.Multiple ? 44 : 16;


    public TableViewColumnsCollection Columns
    {
        get
        {
            return _tableView.Columns;
        }
    }
    private void UpdateTableView()
    {
        if (ItemsSource is not null)
        {
            //Debug.WriteLine(ItemsSource.GetType());
        }
        if (_tableView != null)
        {
            _tableView.IsReadOnly = true;

            //ObservableCollection<SongModelView> mySongs = (ObservableCollection<SongModelView>)ItemsSource;
            if (ItemsSource is null)
            {
                return;
            }

            _tableView.Columns.Clear();
            _tableView.ItemsSource = (System.Collections.IList?)ItemsSource;
            _tableView.Columns.Add(new TableViewTextColumn
            {
                Header = "Title",

                Binding = new Microsoft.UI.Xaml.Data.Binding { Path = new PropertyPath("Title") },

                Width = new GridLength(1, GridUnitType.Star)
            });

            _tableView.Columns.Add(new TableViewTextColumn
            {
                Header = "Artist",
                Binding = new Microsoft.UI.Xaml.Data.Binding { Path = new PropertyPath("ArtistName") },

                Width = new GridLength(1, GridUnitType.Star)
            });
            _tableView.Columns.Add(new TableViewTextColumn
            {
                Header = "Album",
                Binding = new Microsoft.UI.Xaml.Data.Binding { Path = new PropertyPath("AlbumName") },
                Width = new GridLength(1, GridUnitType.Star)
            });
            _tableView.Columns.Add(new TableViewTextColumn()
            {
                Header = "Duration",
                Binding = new Microsoft.UI.Xaml.Data.Binding { Path = new PropertyPath("DurationInSecondsText") },
                //CellTemplate = (DataTemplate)Application.Current.Resources["DurationTemplate"],
                Width = new GridLength(1, GridUnitType.Star)
            });
            _tableView.Columns.Add(new TableViewTextColumn
            {
                Header = "Year",
                Binding = new Microsoft.UI.Xaml.Data.Binding { Path = new PropertyPath("ReleaseYear") },
                Width = new GridLength(1, GridUnitType.Star)
            });
        }
    }

    private void _tableView_AutoGeneratingColumn(object? sender, TableViewAutoGeneratingColumnEventArgs e)
    {
        var viewModel = (object)((TableView)sender).DataContext;


    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

// Default mappers for MauiDataGridHandler
public static class MauiDataGridHandlerMapper
{
    public static IPropertyMapper<MauiDataGrid, MauiDataGridHandler> Mapper =
    new PropertyMapper<MauiDataGrid, MauiDataGridHandler>(ViewHandler.ViewMapper)
    {
        [nameof(MauiDataGrid.ItemsSource)] = MapItemsSource,
        [nameof(MauiDataGrid.AutoGenerateColumns)] = MapAutoGenerateColumns,
        [nameof(MauiDataGrid.CanDragItems)] = MapCanDragItems,
        [nameof(MauiDataGrid.CanReorderItems)] = MapCanReorderItems,
        [nameof(MauiDataGrid.DataFetchSize)] = MapDataFetchSize,
        [nameof(MauiDataGrid.Footer)] = MapFooter,
        [nameof(MauiDataGrid.FooterTemplate)] = MapFooterTemplate,
        [nameof(MauiDataGrid.FooterTransitions)] = MapFooterTransitions,
        [nameof(MauiDataGrid.Header)] = MapHeader,
        [nameof(MauiDataGrid.HeaderTemplate)] = MapHeaderTemplate,
        [nameof(MauiDataGrid.HeaderTransitions)] = MapHeaderTransitions,
        [nameof(MauiDataGrid.IncrementalLoadingThreshold)] = MapIncrementalLoadingThreshold,
        [nameof(MauiDataGrid.IncrementalLoadingTrigger)] = MapIncrementalLoadingTrigger,
        [nameof(MauiDataGrid.IsActiveView)] = MapIsActiveView,
        [nameof(MauiDataGrid.IsItemClickEnabled)] = MapIsItemClickEnabled,
        [nameof(MauiDataGrid.IsMultiSelectCheckBoxEnabled)] = MapIsMultiSelectCheckBoxEnabled,
        [nameof(MauiDataGrid.IsSwipeEnabled)] = MapIsSwipeEnabled,
        [nameof(MauiDataGrid.IsZoomedInView)] = MapIsZoomedInView,
        [nameof(MauiDataGrid.ReorderMode)] = MapReorderMode,
        [nameof(MauiDataGrid.SelectionMode)] = MapSelectionMode,
        [nameof(MauiDataGrid.ShowsScrollingPlaceholders)] = MapShowsScrollingPlaceholders,
        [nameof(MauiDataGrid.HeaderRowHeight)] = MapHeaderRowHeight,
        [nameof(MauiDataGrid.RowHeight)] = MapRowHeight,
        [nameof(MauiDataGrid.RowMaxHeight)] = MapRowMaxHeight,
        [nameof(MauiDataGrid.ShowExportOptions)] = MapShowExportOptions,
        [nameof(MauiDataGrid.SingleSelectionFollowsFocus)] = MapSingleSelectionFollowsFocus,
        // Additional native property mappings:
        [nameof(MauiDataGrid.Columns)] = MapColumns,

        [nameof(MauiDataGrid.IsReadOnly)] = MapIsReadOnly,
        //[nameof(MauiDataGrid.CornerButtonMode)] = MapCornerButtonMode,
        [nameof(MauiDataGrid.CanResizeColumns)] = MapCanResizeColumns,
        [nameof(MauiDataGrid.CanSortColumns)] = MapCanSortColumns,
        [nameof(MauiDataGrid.CanFilterColumns)] = MapCanFilterColumns,
        [nameof(MauiDataGrid.MinColumnWidth)] = MapMinColumnWidth,
        [nameof(MauiDataGrid.MaxColumnWidth)] = MapMaxColumnWidth,
        [nameof(MauiDataGrid.SelectionUnit)] = MapSelectionUnit,
        [nameof(MauiDataGrid.RowContextFlyout)] = MapRowContextFlyout,
        [nameof(MauiDataGrid.CellContextFlyout)] = MapCellContextFlyout,
        [nameof(MauiDataGrid.ColumnHeaderStyle)] = MapColumnHeaderStyle,
        [nameof(MauiDataGrid.CellStyle)] = MapCellStyle

    };

    public static CommandMapper<MauiDataGrid, MauiDataGridHandler> CommandMapper =
        new CommandMapper<MauiDataGrid, MauiDataGridHandler>(ViewHandler.ViewCommandMapper);

    static void MapColumns(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
        {
            //native.Columns.Clear();
            if (native.Columns is not null)
            {
                native.Columns.Clear();
                if (view.Columns is not null)
                {
                    foreach (TableViewColumn col in view.Columns)
                    {
                        //col.SetOwningCollection(native.Columns);
                        //col.SetOwningTableView(native);
                        //col.SetOwningTableView(native);
                        native.Columns.Add(col);
                    }
                }

            }


        }
    }


    public static void MapSelectedItem(MauiDataGridHandler handler, MauiDataGrid view)
    {

        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.SelectedItemProperty, view.SelectedItem);
    }


    public static void MapSelectedIndex(MauiDataGridHandler handler, MauiDataGrid view)
    {

        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.SelectedIndexProperty, view.SelectedIndex);
    }


    public static void MapSelectedValue(MauiDataGridHandler handler, MauiDataGrid view)
    {

        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.SelectedValueProperty, view.SelectedValue);
    }

    public static void MapScrollIntoView(MauiDataGridHandler handler, MauiDataGrid view)
    {

        //var native = GetNativeTable(handler);
        //if (native != null)
        //    native.SetValue(TableView.scro, view.HeaderRowHeight);
        //if (handler.PlatformView is not null && view.ScrollIntoView != null)
        //{
        //    // Ensure the item is in the ItemsSource
        //    if (view.ItemsSource is IEnumerable items && view.ScrollIntoView is not null)
        //    {
        //        foreach (var item in items)
        //        {
        //            if (item == view.ScrollIntoView)
        //            {
        //                handler.PlatformView.ScrollIntoView(item);
        //                break; // Exit once found.
        //            }
        //        }
        //    }
        //}
    }
    static void MapHeaderRowHeight(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.HeaderRowHeightProperty, view.HeaderRowHeight);
    }


    static void MapRowHeight(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.RowHeightProperty, view.RowHeight);
    }

    static void MapRowMaxHeight(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.RowMaxHeightProperty, view.RowMaxHeight);
    }

    static void MapShowExportOptions(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.ShowExportOptionsProperty, view.ShowExportOptions);
    }

    static TableViewImplementation GetNativeTable(MauiDataGridHandler handler)
    {
        if (handler.PlatformView is Microsoft.UI.Xaml.Controls.Grid grid)
            return grid.Children.FirstOrDefault() as TableViewImplementation;
        return null;
    }

    static void MapItemsSource(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.ItemsSource = view.ItemsSource;
    }
    static void MapAutoGenerateColumns(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.AutoGenerateColumns = view.AutoGenerateColumns;
    }
    static void MapCanDragItems(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.CanDragItems = view.CanDragItems;
    }
    static void MapCanReorderItems(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.CanReorderItems = view.CanReorderItems;
    }
    static void MapDataFetchSize(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.DataFetchSize = view.DataFetchSize;
    }
    static void MapFooter(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.Footer = view.Footer;
    }
    static void MapFooterTemplate(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.FooterTemplate = DataTemplateConversion.ConvertToWindowsDataTemplate(view.FooterTemplate);
    }
    static void MapFooterTransitions(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.FooterTransitions = view.FooterTransitions;
    }
    static void MapHeader(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.Header = view.Header;
    }
    static void MapHeaderTemplate(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.HeaderTemplate = DataTemplateConversion.ConvertToWindowsDataTemplate(view.HeaderTemplate);
    }
    static void MapHeaderTransitions(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.HeaderTransitions = view.HeaderTransitions;
    }
    static void MapIncrementalLoadingThreshold(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IncrementalLoadingThreshold = view.IncrementalLoadingThreshold;
    }
    static void MapIncrementalLoadingTrigger(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IncrementalLoadingTrigger = view.IncrementalLoadingTrigger;
    }
    static void MapIsActiveView(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IsActiveView = view.IsActiveView;
    }
    static void MapIsItemClickEnabled(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IsItemClickEnabled = view.IsItemClickEnabled;
    }
    static void MapIsMultiSelectCheckBoxEnabled(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IsMultiSelectCheckBoxEnabled = view.IsMultiSelectCheckBoxEnabled;
    }
    static void MapIsSwipeEnabled(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IsSwipeEnabled = view.IsSwipeEnabled;
    }
    static void MapIsZoomedInView(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.IsZoomedInView = view.IsZoomedInView;
    }
    static void MapReorderMode(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.ReorderMode = view.ReorderMode;
    }
    static void MapSelectionMode(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SelectionMode = view.SelectionMode;
    }

    static void MapShowsScrollingPlaceholders(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.ShowsScrollingPlaceholders = view.ShowsScrollingPlaceholders;
    }
    static void MapSingleSelectionFollowsFocus(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SingleSelectionFollowsFocus = view.SingleSelectionFollowsFocus;
    }

    static void MapIsReadOnly(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.IsReadOnlyProperty, view.IsReadOnly);
    }
    //static void MapCornerButtonMode(MauiDataGridHandler handler, MauiDataGrid view)
    //{
    //    var native = GetNativeTable(handler);
    //    if (native != null)
    //        native.SetValue(TableView.ShowOptionsButtonProperty, view.CornerButtonMode);
    //}
    static void MapCanResizeColumns(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.CanResizeColumnsProperty, view.CanResizeColumns);
    }
    static void MapCanSortColumns(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.CanSortColumnsProperty, view.CanSortColumns);
    }
    static void MapCanFilterColumns(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.CanFilterColumnsProperty, view.CanFilterColumns);
    }
    static void MapMinColumnWidth(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.MinColumnWidthProperty, view.MinColumnWidth);
    }
    static void MapMaxColumnWidth(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.MaxColumnWidthProperty, view.MaxColumnWidth);
    }
    static void MapSelectionUnit(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.SelectionUnitProperty, view.SelectionUnit);
    }

    static void MapRowContextFlyout(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.ContextFlyoutProperty, view.RowContextFlyout);
    }
    static void MapCellContextFlyout(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.ContextFlyoutProperty, view.CellContextFlyout);
    }
    static void MapColumnHeaderStyle(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.StyleProperty, view.ColumnHeaderStyle);
    }
    static void MapCellStyle(MauiDataGridHandler handler, MauiDataGrid view)
    {
        var native = GetNativeTable(handler);
        if (native != null)
            native.SetValue(TableView.StyleProperty, view.CellStyle);
    }
}

public class MauiDataGridHandler : ViewHandler<MauiDataGrid, WinUIGrid>
{
    // Parameterless constructor required for reflection/DI.
    public MauiDataGridHandler()
        : base(MauiDataGridHandlerMapper.Mapper, MauiDataGridHandlerMapper.CommandMapper)
    {
    }

    protected override WinUIGrid CreatePlatformView()
    {
        var gridView = new WinUIGrid();
        gridView.DoubleTapped += GridView_DoubleTapped;

        gridView.Tapped += GridView_Tapped;

        return gridView;
        //return new WinUIGrid();
    }

    private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
    {
        Debug.WriteLine(sender.GetType());
    }

    private void GridView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        Debug.WriteLine(sender.GetType());
    }

    protected override void ConnectHandler(WinUIGrid platformView)
    {
        base.ConnectHandler(platformView);
        if (VirtualView != null)
        {
            var nativeTable = new TableViewImplementation
            {

                ItemsSource = VirtualView.ItemsSource,
                AutoGenerateColumns = VirtualView.AutoGenerateColumns,
                CanDragItems = VirtualView.CanDragItems,
                CanReorderItems = VirtualView.CanReorderItems,
                DataFetchSize = VirtualView.DataFetchSize,
                Footer = VirtualView.Footer,

                //FooterTemplate = VirtualView.FooterTemplate,
                FooterTransitions = VirtualView.FooterTransitions,
                Header = VirtualView.Header,
                //HeaderTemplate = VirtualView.HeaderTemplate,
                HeaderTransitions = VirtualView.HeaderTransitions,
                IncrementalLoadingThreshold = VirtualView.IncrementalLoadingThreshold,
                IncrementalLoadingTrigger = VirtualView.IncrementalLoadingTrigger,
                IsActiveView = VirtualView.IsActiveView,
                IsItemClickEnabled = VirtualView.IsItemClickEnabled,
                IsMultiSelectCheckBoxEnabled = VirtualView.IsMultiSelectCheckBoxEnabled,
                IsSwipeEnabled = VirtualView.IsSwipeEnabled,
                IsRightTapEnabled = VirtualView.IsRightClickEnabled,
                IsZoomedInView = VirtualView.IsZoomedInView,
                ReorderMode = VirtualView.ReorderMode,
                SelectionMode = VirtualView.SelectionMode,

                ShowsScrollingPlaceholders = VirtualView.ShowsScrollingPlaceholders,
                SingleSelectionFollowsFocus = VirtualView.SingleSelectionFollowsFocus
            };
            //nativeTable.TestAg += NativeTable_TestAg;
            // Subscribe to native events:
            nativeTable.Loaded += NativeTable_Loaded;
            nativeTable.Unloaded += NativeTable_Unloaded;
            nativeTable.DoubleTapped += NativeTable_DoubleTapped;
            nativeTable.PointerEntered += NativeTable_PointerEntered;
            nativeTable.PointerExited += NativeTable_PointerExited;
            nativeTable.PointerMoved += NativeTable_PointerMoved;
            nativeTable.PointerPressed += NativeTable_PointerPressed;
            nativeTable.PointerReleased += NativeTable_PointerReleased;
            nativeTable.PointerWheelChanged += NativeTable_PointerWheelChanged;
            nativeTable.RightTapped += NativeTable_RightTapped;
            nativeTable.Tapped += NativeTable_Tapped;
            nativeTable.CharacterReceived += NativeTable_CharacterReceived;
            nativeTable.ContextRequested += NativeTable_ContextRequested;
            nativeTable.DataContextChanged += NativeTable_DataContextChanged;
            nativeTable.PropertyChanged += NativeTable_PropertyChanged;
            nativeTable.SelectionChanged += NativeTable_SelectionChanged;
            //nativeTable.PropertyChanged += NativeTable_PropertyChanged;
            platformView.Children.Add(nativeTable);


        }
    }

    private void NativeTable_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        VirtualView.RaiseSelectionChanged(e);
    }

    protected override void DisconnectHandler(WinUIGrid platformView)
    {
        if (platformView.Children.FirstOrDefault() is TableViewImplementation nativeTable)
        {
            nativeTable.Loaded -= (s, e) => VirtualView?.RaiseLoaded(e);
            nativeTable.Unloaded -= (s, e) => VirtualView?.RaiseUnloaded(e);
            nativeTable.DoubleTapped -= (s, e) => VirtualView?.RaiseDoubleTapped(e);
            nativeTable.PointerEntered -= (s, e) => VirtualView?.RaisePointerEntered(e);
            nativeTable.PointerExited -= (s, e) => VirtualView?.RaisePointerExited(e);
            nativeTable.PointerMoved -= (s, e) => VirtualView?.RaisePointerMoved(e);
            nativeTable.PointerPressed -= (s, e) => VirtualView?.RaisePointerPressed(e);
            nativeTable.PointerReleased -= (s, e) => VirtualView?.RaisePointerReleased(e);
            nativeTable.PointerWheelChanged -= (s, e) => VirtualView?.RaisePointerWheelChanged(e);
            nativeTable.RightTapped -= (s, e) => VirtualView?.RaiseRightTapped(e);
            nativeTable.Tapped -= (s, e) => VirtualView?.RaiseTapped(e);
            nativeTable.CharacterReceived -= (s, e) => VirtualView?.RaiseCharacterReceived(e);
            nativeTable.ContextRequested -= (s, e) => VirtualView?.RaiseContextRequested(e);
            nativeTable.DataContextChanged -= (s, e) => VirtualView?.RaiseDataContextChanged(this, e);
            nativeTable.PropertyChanged -= (s, e) => VirtualView?.RaisePropertyChanged(this, e);
            //nativeTable.SizeChanged -= (s, e) => VirtualView?.RaiseSizeChanged(e);
            nativeTable.Holding -= (s, e) => VirtualView?.RaiseHolding(e);
            nativeTable.KeyDown -= (s, e) => VirtualView?.RaiseKeyDown(e);
            nativeTable.KeyUp -= (s, e) => VirtualView?.RaiseKeyUp(e);

        }
        base.DisconnectHandler(platformView);
    }

    // Event forwarding methods:
    private void NativeTable_Loaded(object sender, RoutedEventArgs e)
    {
        VirtualView?.RaiseLoaded(e);
    }

    //private void NativeTable_TestAg(object sender, RoutedEventArgs e) => this.

    private void NativeTable_Unloaded(object sender, RoutedEventArgs e) => VirtualView?.RaiseUnloaded(e);

    private void NativeTable_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        VirtualView?.RaiseDoubleTapped(e);
    }

    private void NativeTable_PointerEntered(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerEntered(e);


    private void NativeTable_PointerExited(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerExited(e);

    private void NativeTable_PointerMoved(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerMoved(e);

    private void NativeTable_PointerPressed(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerPressed(e);

    private void NativeTable_PointerReleased(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerReleased(e);

    private void NativeTable_PointerWheelChanged(object sender, PointerRoutedEventArgs e) => VirtualView?.RaisePointerWheelChanged(e);

    private void NativeTable_RightTapped(object sender, RightTappedRoutedEventArgs e) => VirtualView?.RaiseRightTapped(e);

    private void NativeTable_Tapped(object sender, TappedRoutedEventArgs e) => VirtualView?.RaiseTapped(e);

    private void NativeTable_CharacterReceived(object sender, CharacterReceivedRoutedEventArgs e) => VirtualView?.RaiseCharacterReceived(e);

    private void NativeTable_ContextRequested(object sender, ContextRequestedEventArgs e) => VirtualView?.RaiseContextRequested(e);

    private void NativeTable_DataContextChanged(object sender, DataContextChangedEventArgs e)
    {
        VirtualView?.RaiseDataContextChanged(this, e);
    }

    private void NativeTable_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        VirtualView?.RaisePropertyChanged(this, e);
    }
}

public partial class TableViewColumnsCollection : ObservableCollection<TableViewColumn>
{
   
}
//public static class DataGridHandler
//{
//    public static void ConfigureTableViewHandler(IMauiHandlersCollection handlers)
//    {
//        handlers.AddHandler(typeof(MauiDataGrid), typeof(MauiDataGridHandler));
//    }
//}
