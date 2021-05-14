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
        private readonly BackUpAbstractLogic _backUpAbstractLogic;

        public FormMain(OrderLogic orderLogic, ReportLogic Report, BackUpAbstractLogic backUpAbstractLogic)
        {
            InitializeComponent();
            this._orderLogic = orderLogic;
            report = Report;
            _backUpAbstractLogic = backUpAbstractLogic;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                Program.ConfigGrid(_orderLogic.Read(null), dataGridView);

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

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void исполнителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormImplementers>();
            form.ShowDialog();
        }

        private void запускРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var workModeling = Container.Resolve<WorkModeling>();
            workModeling.DoWork();
            MessageBox.Show("Работы запущены", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void письмаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMails>();
            form.ShowDialog();
        }

        private void создатьБекапToolStripMenuItem_Click(object sender, EventArgs e)
        {
                try
                {
                    if (_backUpAbstractLogic != null)
                    {
                        var fbd = new FolderBrowserDialog();
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            _backUpAbstractLogic.CreateArchive(fbd.SelectedPath);
                            MessageBox.Show("Бекап создан", "Сообщение",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
    }
}
