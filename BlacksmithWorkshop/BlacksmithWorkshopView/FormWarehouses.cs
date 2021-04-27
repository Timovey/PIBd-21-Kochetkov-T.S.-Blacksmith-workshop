using System;
using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using Unity;
using System.Windows.Forms;

namespace BlacksmithWorkshopView
{
    public partial class FormWarehouses : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly WarehouseLogic _logicW;
        public FormWarehouses(WarehouseLogic logicW)
        {
            InitializeComponent();
            _logicW = logicW;
        }
        private void FormWarehouses_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _logicW.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[4].Visible = false;

                    dataGridView.Columns[1].AutoSizeMode =
DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[2].AutoSizeMode =
DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView.Columns[3].AutoSizeMode =
DataGridViewAutoSizeColumnMode.Fill;         
                }

    

                dataGridViewComponents.Rows.Clear();
                if (dataGridView.SelectedRows.Count == 1)
                {
                    int compId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);

                        var listComp = _logicW.Read(new WarehouseBindingModel
                        {
                            Id = compId
                        })?[0];
                    if(listComp != null)
                    {
                        foreach (var lc in listComp.WerehouseComponents)
                        {
                            dataGridViewComponents.Rows.Add(new object[] {
                           lc.Key, lc.Value.Item1, lc.Value.Item2
                    });
                        }
                    }
    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void LoadDataGridComponents()
        {

            dataGridViewComponents.Rows.Clear();
            if (dataGridView.SelectedRows.Count == 1)
            {
                int compId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);

                var listComp = _logicW.Read(new WarehouseBindingModel
                {
                    Id = compId
                })?[0];
                if (listComp != null)
                {
                    foreach (var lc in listComp.WerehouseComponents)
                    {
                        dataGridViewComponents.Rows.Add(new object[] {
                           lc.Key, lc.Value.Item1, lc.Value.Item2
                    });
                    }
                }

            }
        }
        private void DataGridView_SelectedRow(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouse>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormWarehouse>();
                form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id =
                   Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logicW.Delete(new WarehouseBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadData();
        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            LoadDataGridComponents();
        }
    }
}
