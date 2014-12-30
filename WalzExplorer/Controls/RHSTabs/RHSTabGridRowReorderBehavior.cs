
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;

namespace WalzExplorer.Controls.RHSTabs
{
    /// <summary>
    /// Provides row (item) reoder capabilities to RadGridView
    /// </summary>
    public partial class RHSTabGridRowReorderBehavior
    {
        private RadGridView _associatedObject;
        /// <summary>
        /// AssociatedObject Property
        /// </summary>
        public RadGridView AssociatedObject
        {
            get
            {
                return _associatedObject;
            }
            set
            {
                _associatedObject = value;
            }
        }

        private static Dictionary<RadGridView, RHSTabGridRowReorderBehavior> instances;

        static RHSTabGridRowReorderBehavior()
        {
            instances = new Dictionary<RadGridView, RHSTabGridRowReorderBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            RHSTabGridRowReorderBehavior behavior = GetAttachedBehavior(obj as RadGridView);

            behavior.AssociatedObject = obj as RadGridView;

            if (value)
            {
                behavior.Initialize();
            }
            else
            {
                behavior.CleanUp();
            }
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(RHSTabGridRowReorderBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static RHSTabGridRowReorderBehavior GetAttachedBehavior(RadGridView gridview)
        {
            if (!instances.ContainsKey(gridview))
            {
                instances[gridview] = new RHSTabGridRowReorderBehavior();
                instances[gridview].AssociatedObject = gridview;
            }

            return instances[gridview];
        }

        protected virtual void Initialize()
        {
            this.AssociatedObject.RowLoaded -= this.AssociatedObject_RowLoaded;
            this.AssociatedObject.RowLoaded += this.AssociatedObject_RowLoaded;
            this.UnsubscribeFromDragDropEvents();
            this.SubscribeToDragDropEvents();
        }

        protected virtual void CleanUp()
        {
            this.AssociatedObject.RowLoaded -= this.AssociatedObject_RowLoaded;
            this.UnsubscribeFromDragDropEvents();
        }

        void AssociatedObject_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row is GridViewHeaderRow || e.Row is GridViewNewRow || e.Row is GridViewFooterRow)
                return;

            GridViewRow row = e.Row as GridViewRow;
            this.InitializeRowDragAndDrop(row);
        }

        private void InitializeRowDragAndDrop(GridViewRow row)
        {
            if (row == null)
                return;

            DragDropManager.RemoveDragOverHandler(row, OnRowDragOver);
            DragDropManager.AddDragOverHandler(row, OnRowDragOver);
        }

        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop);
        }

        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(this.AssociatedObject, OnDrop);
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            RadGridView grd = (e.OriginalSource as FrameworkElement).ParentOfType<RadGridView>();
            if (grd.SelectedItems!=null)
            {
                DropIndicationDetails details = new DropIndicationDetails();
                var itemList = grd.SelectedItems;
                details.CurrentDraggedItem = itemList;

                IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

                dragPayload.SetData("DraggedItem", itemList);
                dragPayload.SetData("DropDetails", details);

                e.Data = dragPayload;

                e.DragVisual = new DragVisual()
                {
                    Content = details,
                    ContentTemplate = this.AssociatedObject.Resources["DraggedItemTemplate"] as DataTemplate
                };
                e.DragVisualOffset = e.RelativeStartPoint;
                e.AllowedEffects = DragDropEffects.All;
                
                //MyViewModel viewModel = (MyViewModel)((sender as RadGridView).DataContext);
                ////foreach (var item in draggedItems)
                //viewModel.Insert((tblMain)itemList[0], 0);

            }

            ////ORIGINAL
            //var sourceRow = e.OriginalSource as GridViewRow ?? (e.OriginalSource as FrameworkElement).ParentOfType<GridViewRow>();
            //if (sourceRow != null && sourceRow.Name != "PART_RowResizer")
            //{
            //    DropIndicationDetails details = new DropIndicationDetails();
            //    var item = sourceRow.Item;
            //    details.CurrentDraggedItem = item;

            //    IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

            //    dragPayload.SetData("DraggedItem", item);
            //    dragPayload.SetData("DropDetails", details);

            //    e.Data = dragPayload;

            //    e.DragVisual = new DragVisual()
            //    {
            //        Content = details,
            //        ContentTemplate = this.AssociatedObject.Resources["DraggedItemTemplate"] as DataTemplate
            //    };
            //    e.DragVisualOffset = e.RelativeStartPoint;
            //    e.AllowedEffects = DragDropEffects.All;
            //}
        }

        private void OnGiveFeedback(object sender, Telerik.Windows.DragDrop.GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            ObservableCollection<object> draggedItems = (ObservableCollection<object>)(DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem"));
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItems == null)
            {
                return;
            }

            if (e.Effects == DragDropEffects.Move || e.Effects == DragDropEffects.All)
            {
                //((sender as RadGridView).ItemsSource as IList).Remove(draggedItems);
            }

            if (e.Effects != DragDropEffects.None)
            {
                var collection = (sender as RadGridView).ItemsSource as IList;
                int index = details.DropIndex < 0 ? 0 : details.DropIndex;
                index = details.DropIndex > collection.Count - 1 ? collection.Count : index;

                //collection.Insert(index, draggedItem);
                RadGridView grd = (sender as RadGridView);
                RHSTabGridViewModelBase viewModel = (RHSTabGridViewModelBase)((sender as RadGridView).DataContext);
                //foreach (var item in grd.SelectedItems)
                //ObservableCollection<object> items = grd.SelectedItems;

                viewModel.MoveItemsToIndex(grd.SelectedItems.ToList(), index);
                
              
            }
        }

        private void OnRowDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var row = sender as GridViewRow;
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || row == null)
            {
                return;
            }

            details.CurrentDraggedOverItem = row.DataContext;

            if (details.CurrentDraggedItem == details.CurrentDraggedOverItem)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            details.CurrentDropPosition = GetDropPositionFromPoint(e.GetPosition(row), row);
            int dropIndex = (this.AssociatedObject.Items as IList).IndexOf(row.DataContext);
            int draggedItemIdex = (this.AssociatedObject.Items as IList).IndexOf(DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedItem"));

            if (dropIndex >= row.GridViewDataControl.Items.Count - 1 && details.CurrentDropPosition == DropPosition.After)
            {
                details.DropIndex = dropIndex;
                return;
            }

            dropIndex = draggedItemIdex > dropIndex ? dropIndex : dropIndex - 1;
            details.DropIndex = details.CurrentDropPosition == DropPosition.Before ? dropIndex : dropIndex + 1;
        }

        public virtual DropPosition GetDropPositionFromPoint(Point absoluteMousePosition, GridViewRow row)
        {
            if (row != null)
            {
                return absoluteMousePosition.Y < row.ActualHeight / 2 ? DropPosition.Before : DropPosition.After;
            }

            return DropPosition.Inside;
        }
    }
}

