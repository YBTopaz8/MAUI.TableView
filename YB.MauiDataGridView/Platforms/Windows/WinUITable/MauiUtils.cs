using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Markup;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTemplate = Microsoft.UI.Xaml.DataTemplate;

namespace YB.MauiDataGridView.Platforms.Windows.WinUITable;

public static class DataTemplateConversion
{

    public static DataTemplate ConvertToWindowsDataTemplate(Microsoft.Maui.Controls.DataTemplate mauiTemplate)
    {
        if (mauiTemplate == null)
            return null;

        // Generate a unique id.
        string uniqueId = Guid.NewGuid().ToString();

        // Create XAML that instantiates our MauiDataTemplateHost with its Tag set to the unique id.
        string xaml =
            "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' " +
            "xmlns:local='using:YourNamespace'>" +
            $"<local:MauiDataTemplateHost Tag='{uniqueId}'/>" +
            "</DataTemplate>";

        // Load the WinUI DataTemplate.
        var winDataTemplate = (Microsoft.UI.Xaml.DataTemplate)XamlReader.Load(xaml);

        // Store the MAUI DataTemplate in the helper so that the host can retrieve it when loaded.
        MauiDataTemplateHostHelper.PendingTemplates[uniqueId] = mauiTemplate;

        return winDataTemplate;
    }
}
public static class MauiDataTemplateHostHelper
{
    public static ConcurrentDictionary<string, Microsoft.Maui.Controls.DataTemplate> PendingTemplates { get; } = new ConcurrentDictionary<string, Microsoft.Maui.Controls.DataTemplate>();
}

public partial class MauiDataTemplateHost : ContentControl
{
    public MauiDataTemplateHost()
    {
        this.Loaded += MauiDataTemplateHost_Loaded;
    }

    private void MauiDataTemplateHost_Loaded(object sender, RoutedEventArgs e)
    {
        // If Tag holds a unique id, use it to look up the pending MAUI DataTemplate.
        if (this.Tag is string id)
        {
            if (MauiDataTemplateHostHelper.PendingTemplates.TryRemove(id, out var mauiTemplate))
            {
                MauiTemplate = mauiTemplate;
            }
        }
        ApplyMauiTemplate();
    }

    public Microsoft.Maui.Controls.DataTemplate MauiTemplate { get; set; }

    private void ApplyMauiTemplate()
    {
        if (MauiTemplate == null)
            return;

        // Create the MAUI view from the DataTemplate.
        var content = MauiTemplate.CreateContent();
        if (content is Microsoft.Maui.Controls.View mauiView)
        {
            // Get the current MauiContext from Application.Current.
            var mauiContext = Microsoft.Maui.Controls.Application.Current?.Handler?.MauiContext;
            if (mauiContext == null)
            {
                throw new InvalidOperationException("No MauiContext available.");
            }
            // Convert the MAUI view to a native WinUI view using the MauiContext.
            this.Content = mauiView.ToPlatform(mauiContext);
        }
    }
}