using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace BlacksmithWorkshopView
{
    public partial class FormAdditionToWarehouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly WarehouseLogic _logicW;
        private readonly ComponentLogic _logicС;

        public FormAdditionToWarehouse(WarehouseLogic logicW, ComponentLogic logicC)
        {
            InitializeComponent();
            _logicW = logicW;
            _logicС = logicC;
        }
        private void FormWarehouse_Load(object sender, EventArgs e)
        {
            
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<WarehouseViewModel> listW = _logicW.Read(null);
                if (listW != null)
                {
                    comboBoxWarehouse.DisplayMember = "Name";
                    comboBoxWarehouse.ValueMember = "Id";
                    comboBoxWarehouse.DataSource = listW;
                    comboBoxWarehouse.SelectedItem = null;
                }
                List<ComponentViewModel> listC = _logicС.Read(null);
                if (listC != null)
                {
                    comboBoxComponents.DisplayMember = "ComponentName";
                    comboBoxComponents.ValueMember = "Id";
                    comboBoxComponents.DataSource = listC;
                    comboBoxComponents.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ComboBoxWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponents.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicW.Reffil(new AddToWarehouseBindingModel
                {
                    WarehouseId = Convert.ToInt32(comboBoxWarehouse.SelectedValue),
                    ComponentId = Convert.ToInt32(comboBoxComponents.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }    
    }
}
