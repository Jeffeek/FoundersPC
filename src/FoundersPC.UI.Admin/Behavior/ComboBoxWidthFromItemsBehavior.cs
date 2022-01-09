using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace FoundersPC.UI.Admin.Behavior;

public static class ComboBoxWidthFromItemsBehavior
{
    public static readonly DependencyProperty ComboBoxWidthFromItemsProperty =
        DependencyProperty.RegisterAttached("ComboBoxWidthFromItems",
                                            typeof(bool),
                                            typeof(ComboBoxWidthFromItemsBehavior),
                                            new UIPropertyMetadata(false, OnComboBoxWidthFromItemsPropertyChanged));

    public static bool GetComboBoxWidthFromItems(DependencyObject obj) =>
        (bool)obj.GetValue(ComboBoxWidthFromItemsProperty);

    public static void SetComboBoxWidthFromItems(DependencyObject obj, bool value) =>
        obj.SetValue(ComboBoxWidthFromItemsProperty, value);

    private static void OnComboBoxWidthFromItemsPropertyChanged(DependencyObject dpo,
                                                                DependencyPropertyChangedEventArgs e)
    {
        if (dpo is not ComboBox comboBox)
            return;

        if ((bool)e.NewValue == true)
            comboBox.Loaded += OnComboBoxLoaded;
        else
            comboBox.Loaded -= OnComboBoxLoaded;
    }
    private static void OnComboBoxLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is not ComboBox comboBox)
            return;

        comboBox.Dispatcher.BeginInvoke(() => comboBox.SetWidthFromItems(), DispatcherPriority.ContextIdle);
    }
}

public static class ComboBoxExtensionMethods
{
    public static void SetWidthFromItems(this ComboBox comboBox)
    {
        double comboBoxWidth = 10; // comboBox.DesiredSize.Width;

        // Create the peer and provider to expand the comboBox in code behind.
        var peer = new ComboBoxAutomationPeer(comboBox);

        if (peer.GetPattern(PatternInterface.ExpandCollapse) is not IExpandCollapseProvider provider)
            return;

        void Handler(object o, EventArgs eventArgs)
        {
            if (!comboBox.IsDropDownOpen || comboBox.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                return;

            double width = 0;

            foreach (var item in comboBox.Items)
            {
                if (comboBox.ItemContainerGenerator.ContainerFromItem(item) is not ComboBoxItem comboBoxItem)
                    continue;

                comboBoxItem.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                if (comboBoxItem.DesiredSize.Width > width)
                    width = comboBoxItem.DesiredSize.Width;
            }

            comboBox.Width = comboBoxWidth + width;
            // Remove the event handler.
            comboBox.ItemContainerGenerator.StatusChanged -= Handler;
            comboBox.DropDownOpened -= Handler;
            provider.Collapse();
        }

        comboBox.ItemContainerGenerator.StatusChanged += Handler;
        comboBox.DropDownOpened += Handler;
        // Expand the comboBox to generate all its ComboBoxItem's.
        provider.Expand();
    }
}