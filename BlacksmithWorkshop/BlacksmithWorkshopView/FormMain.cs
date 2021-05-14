using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace BlacksmithWorkshopView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly OrderLogic _orderLogic;
        private readonly ReportLogic report;
        public FormMain(OrderLogic orderLogic, ReportLogic Report)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
            report = Report;
            dataGridView.DataSource = _orderLogic.Read(null);
           

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = _orderLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void КомпонентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComponents>();
            form.ShowDialog();
        }
        private void ИзделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormManufactures>();
            form.ShowDialog();
        }
        private void ButtonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }
        private void ButtonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                    {
                        OrderId =
                   id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.FinishOrder(new ChangeStatusBindingModel
                    {
                        OrderId = id
                    });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    _orderLogic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouses>();
            form.ShowDialog();
        }

        private void пополнениеСкладаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAdditionToWarehouse>();
            form.ShowDialog();
        }


        private void компонентыПоИзделиямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportProductComponents>();
            form.ShowDialog();
        }

        private void списокЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClientOrders>();
            form.ShowDialog();

        }

        private void списокКомпонентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    report.SaveComponentsToWordFile(new ReportBindingModel
                    {
                        FileName =
                   dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                }
            }
        }

        private void списокСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    report.SaveWarehousesToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });

                    MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void компонентыПоСкладамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportWarehouseComponents>();
            form.ShowDialog();
        }

        private void списокЗаказовПоДатамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportOrderByDate>();
            form.ShowDialog();
        }
    }
}
