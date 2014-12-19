using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace DragDrop
{
    public class DropIndicationDetails : ViewModelBase
    {
        private object currentDraggedItem;
        private string currentDraggedItemText;
        private DropPosition currentDropPosition;
        private object currentDraggedOverItem;

        public object CurrentDraggedOverItem
        {
            get
            {
                return currentDraggedOverItem;
            }
            set
            {
                if (this.currentDraggedOverItem != value)
                {
                    currentDraggedOverItem = value;
                    OnPropertyChanged("CurrentDraggedOverItem");
                }
            }
        }

        public int DropIndex { get; set; }

        public DropPosition CurrentDropPosition
        {
            get
            {
                return this.currentDropPosition;
            }
            set
            {
                if (this.currentDropPosition != value)
                {
                    this.currentDropPosition = value;
                    OnPropertyChanged("CurrentDropPosition");
                }
            }
        }

        public object CurrentDraggedItem
        {
            get
            {
                return this.currentDraggedItem;
            }
            set
            {
                if (this.currentDraggedItem != value)
                {
                    this.currentDraggedItem = value;
                    OnPropertyChanged("CurrentDraggedItem");
                }
            }
        }
        public string CurrentDraggedItemText
        {
            get
            {
                return this.currentDraggedItemText;
            }
            set
            {
                if (this.currentDraggedItemText != value)
                {
                    this.currentDraggedItemText = value;
                    OnPropertyChanged("CurrentDraggedItemText");
                }
            }
        }
    }
}
