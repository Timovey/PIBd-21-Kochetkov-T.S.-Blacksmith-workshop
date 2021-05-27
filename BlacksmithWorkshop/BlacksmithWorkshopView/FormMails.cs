using System.Collections.Generic;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using BlacksmithWorkshopBusinessLogic.BindingModels;

namespace BlacksmithWorkshopView
{
    public partial class FormMails : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly MailLogic logic;
        private int page;
        public FormMails(MailLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            page = 1;
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(int page = 1)
        {
            try
            {
                var list = logic.GetMessagesPage(new MessageInfoBindingModel
                {
                    Page = page,
                    PageSize = Program.pageSize
                });
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            int max = (logic.Count() - 1) / Program.pageSize + 1;
            page--;
            if (page > max || page < 1)
            {
                page++;
                MessageBox.Show("Нет такой страницы" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            LoadData(page);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            int max = (logic.Count() - 1) / Program.pageSize + 1;
            page++;
            if (page > max || page < 1)
            {
                page--;
                MessageBox.Show("Нет такой страницы" , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData(page);
        }

        private void buttonPage_Click(object sender, EventArgs e)
        {
            int max = (logic.Count() - 1) / Program.pageSize + 1;
            if (string.IsNullOrEmpty(textBoxPage.Text))
            {
                MessageBox.Show("Заполнитестраницу перехода", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                page = Convert.ToInt32(textBoxPage.Text);
                if (page > max || page < 1)
                {
                    MessageBox.Show("Нет такой страницы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadData(page);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
