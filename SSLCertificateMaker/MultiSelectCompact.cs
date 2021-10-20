using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSLCertificateMaker
{
	[DefaultEvent("SelectedItemsChanged")]
	public partial class MultiSelectCompact : UserControlBase
	{
		protected override Control SnapLineControl
		{
			get
			{
				return btnEdit;
			}
		}

		/// <summary>
		/// Gets or sets the array of items available for selection.  When setting the array, all current selections will be cleared.
		/// </summary>
		public object[] Items
		{
			get
			{
				return _items;
			}
			set
			{
				_items = value;
				SelectedIndices = null;
			}
		}
		/// <summary>
		/// The array of items available for selection.
		/// </summary>
		private object[] _items;
		/// <summary>
		/// Gets an array of items that are currently selected.
		/// </summary>
		public object[] SelectedItems
		{
			get
			{
				List<object> selected = new List<object>();
				if (_items != null)
					for (int i = 0; i < _items.Length; i++)
						if (_selectedIndices[i])
							selected.Add(Items[i]);
				return selected.ToArray();
			}
		}
		/// <summary>
		/// Gets or sets an array indicating which indices are selected. Setting a null value will actually cause a new array of appropriate length to be set instead.
		/// </summary>
		public bool[] SelectedIndices
		{
			get
			{
				return _selectedIndices;
			}
			set
			{
				bool[] v = value;
				if (v == null)
					v = new bool[_items == null ? 0 : _items.Length];
				if (_selectedIndices == null || string.Join(",", _selectedIndices) != string.Join(",", v))
				{
					_selectedIndices = v;
					UpdateSelectionsLabel();
					SelectedItemsChanged(this, new EventArgs());
				}
			}
		}
		/// <summary>
		/// The array indicating which indices are selected.
		/// </summary>
		private bool[] _selectedIndices;
		/// <summary>
		/// Event raised when the selected items have changed.
		/// </summary>
		public event EventHandler SelectedItemsChanged = delegate { };

		public MultiSelectCompact()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes the control. Must be called before the user clicks the edit button.
		/// </summary>
		/// <param name="text">Title associated with the items. Such as "Recipes" or "Acceptable Colors". This gets assigned to the "Text" property.</param>
		/// <param name="items">Items to make available for selection. See <see cref="Items"/> and <see cref="SelectedItems"/>.</param>
		/// <param name="selectedIndices">An array indicating which indices are selected. See <see cref="SelectedIndices"/> and <see cref="SelectedItems"/>.</param>
		public void Initialize<T>(string text, MultiSelectListItem<T>[] items, bool[] selectedIndices = null)
		{
			// This overload exists only to hint that MultiSelectListItem<T> is an available item class.
			Initialize(text, (object[])items, selectedIndices);
		}

		/// <summary>
		/// Initializes the control. Must be called before the user clicks the edit button.
		/// </summary>
		/// <param name="text">Title associated with the items. Such as "Recipes" or "Acceptable Colors". This gets assigned to the "Text" property.</param>
		/// <param name="items">Items to make available for selection. Each should override ToString(). See <see cref="Items"/> and <see cref="SelectedItems"/>.</param>
		/// <param name="selectedIndices">An array indicating which indices are selected. See <see cref="SelectedIndices"/> and <see cref="SelectedItems"/>.</param>
		public void Initialize(string text, object[] items, bool[] selectedIndices = null)
		{
			if (selectedIndices == null)
				selectedIndices = new bool[items.Length];
			if (items.Length != selectedIndices.Length)
				throw new ArgumentException("items.Length and selectdIndices.Length must match");
			Text = text;
			_items = new object[items.Length];
			for (int i = 0; i < items.Length; i++)
				_items[i] = items[i];
			_selectedIndices = selectedIndices.ToArray();
			UpdateSelectionsLabel();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (_items == null || _selectedIndices == null || _items.Length != _selectedIndices.Length)
				throw new ApplicationException("MultiSelectCompact was not initialized prior to use.");
			EditMultiSelect ems = new EditMultiSelect("Select " + Text, _items, _selectedIndices);
			if (ems.ShowDialog() == DialogResult.OK)
				SelectedIndices = ems.SelectedIndices;
		}
		private void UpdateSelectionsLabel()
		{
			lblSelections.Text = string.Join(", ", SelectedItems);
		}
	}
}

