using ProjectPRN.Models;

namespace ProjectPRN
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        MyOrderContext context = new MyOrderContext();
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text != String.Empty || txtPass.Text != String.Empty)
            {
                var user = context.TblUsers.FirstOrDefault(item => item.Username==txtName.Text);
                if (user != null)
                {
                    if (user.Pass== Int32.Parse(txtPass.Text))
                    {
                        MessageBox.Show("Login success");
                        Main main = new Main();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Password is incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("Username is incorrect");
                }
            }
            else
            {
                MessageBox.Show("UserName and Password required");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var user = context.TblUsers;
        }
        public void GetMessage(string Message, string pw)
        {
            txtName.Text = Message;
            txtPass.Text = pw;
        }
    }
}