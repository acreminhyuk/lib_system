﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FINAL_project_LibraryProgram_1234
{
    public partial class Master : Form
    {
        // <MySQL 연결 변수>
        MySqlConnection connection = new MySqlConnection("Server = localhost;Database=library_project;Uid=root;Pwd=root;");
        public Master()
        {
            InitializeComponent();
        }

        // <문의하기 픽쳐박스 클릭시>
        private void picture_support_Click(object sender, EventArgs e)
        {
            SupportMail showSupportMail = new SupportMail();
            showSupportMail.ShowDialog();
        }

        // 로그아웃 픽쳐박스 클릭시
        private void picture_logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("로그아웃 후 메인화면으로 돌아가시겠습니까?", "로그아웃", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                Main showMain = new Main();
                showMain.Show();
            }
        }

        // <프로그램 종료 픽쳐박스 클릭시>
        private void picture_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("일이삼사 도서관리 프로그램을 정말 종료하시곘습니까?", "프로그램 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // <탭 1에서, 조회/재조회 버튼 클릭시>
        private void btn_tab1_load_Click(object sender, EventArgs e)
        {
            string insertQuery_loadMember = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member;";
            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery_loadMember, connection);

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable load_data_tab1_member = new DataTable();
                adapter.Fill(load_data_tab1_member);
                data_tab1_member.DataSource = load_data_tab1_member;

                string insertQuery_loadBook = "SELECT * FROM library_project.book";
                MySqlCommand command2 = new MySqlCommand(insertQuery_loadBook, connection);
                MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2);
                DataTable load_data_tab1_book = new DataTable();
                adapter2.Fill(load_data_tab1_book);
                data_tab1_book.DataSource = load_data_tab1_book;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL 연결 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다. \n\n오류내용 : " + ex.Message, "조회 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        // <탭 1에서, 회원목록 셀 클릭 시>
        private void data_tab1_member_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtbox_tab1_membernum.Text = data_tab1_member.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtbox_tab1_name.Text = data_tab1_member.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtbox_tab1_id.Text = data_tab1_member.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtbox_tab1_loannum.Text = data_tab1_member.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtbox_tab1_memberstauts.Text = data_tab1_member.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtbox_tab1_gender.Text = data_tab1_member.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtbox_tab1_membirth.Text = data_tab1_member.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtbox_tab1_tel.Text = data_tab1_member.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtbox_tab1_address.Text = data_tab1_member.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
            catch (Exception)
            {
                // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
            }
        }

        // <탭 1에서, 도서목록 셀 클릭 시>
        private void data_tab1_book_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtbox_tab1_booknum.Text = data_tab1_book.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtbox_tab1_bookname.Text = data_tab1_book.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtbox_tab1_bookisbn.Text = data_tab1_book.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtbox_tab1_writer.Text = data_tab1_book.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtbox_tab1_date.Text = data_tab1_book.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtbox_tab1_publisher.Text = data_tab1_book.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtbox_tab1_bookstatus.Text = data_tab1_book.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtbox_tab1_loan.Text = data_tab1_book.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtbox_tab1_price.Text = data_tab1_book.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtbox_tab1_pages.Text = data_tab1_book.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtbox_tab1_bookinfo.Text = data_tab1_book.Rows[e.RowIndex].Cells[10].Value.ToString();

                string wholoanbook = data_tab1_book.Rows[e.RowIndex].Cells[11].Value.ToString();
                if (wholoanbook == "")
                {
                    txtbox_tab1_wholoanbook.Text = "대출 가능 도서입니다.";
                }
                else
                {
                    txtbox_tab1_wholoanbook.Text = wholoanbook;
                }
            }
            catch (Exception)
            {
                // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
            }
        }

        // <탭 1에서, 회원검색 (분류) 선택 시, 회원번호/연락처/생년월일을 선택한다면 양식에 맞게 작성하도록 maskedtxtbox와 연결해줌>
        private void combobox_tab1_searchmem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_tab1_searchmem.SelectedItem.ToString() == "회원번호")
            {
                txtbox_tab1_searchmem.Visible = false;
                txtbox_tab1_searchmem.Text = "";

                maskedtxtBox_tab1_searchmem.Visible = true;
                maskedtxtBox_tab1_searchmem.Mask = "000000000000";
            }
            else if (combobox_tab1_searchmem.SelectedItem.ToString() == "연락처")
            {
                txtbox_tab1_searchmem.Visible = false;
                txtbox_tab1_searchmem.Text = "";

                maskedtxtBox_tab1_searchmem.Visible = true;
                maskedtxtBox_tab1_searchmem.Mask = "000-0000-0000";
            }
            else if (combobox_tab1_searchmem.SelectedItem.ToString() == "생년월일")
            {
                txtbox_tab1_searchmem.Visible = false;
                txtbox_tab1_searchmem.Text = "";

                maskedtxtBox_tab1_searchmem.Visible = true;
                maskedtxtBox_tab1_searchmem.Mask = "0000-00-00";
            }
            else
            {
                maskedtxtBox_tab1_searchmem.Visible = false;
                maskedtxtBox_tab1_searchmem.Text = "";

                txtbox_tab1_searchmem.Visible = true;
            }
        }

        // <탭 1에서, 회원검색 버튼 클릭시>
        private void btn_tab1_memsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_tab1_searchmem.Text == "" && maskedtxtBox_tab1_searchmem.Text == "")
                {
                    MessageBox.Show("검색명 입력 후 시도 바랍니다.");
                }
                else
                {
                    if (combobox_tab1_searchmem.Text =="회원번호")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 회원번호 = '" + maskedtxtBox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchmem.Text == "이름")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 이름 = '" + txtbox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchmem.Text == "아이디")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 아이디 = '" + txtbox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchmem.Text == "연락처")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 연락처 = '" + maskedtxtBox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchmem.Text == "생년월일")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 생년월일 = '" + maskedtxtBox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchmem.Text == "이메일")
                    {
                        string insertQuery = "SELECT 회원번호, 이름, 아이디, 대출권수, 회원상태, 성별, 생년월일, 연락처, 주소 FROM library_project.member WHERE 이메일 = '" + txtbox_tab1_searchmem.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_member = new DataTable();
                        adapter.Fill(load_data_tab1_member);
                        data_tab1_member.DataSource = load_data_tab1_member;
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
            }
        }

        // <탭 1에서, 도서검색 (분류) 선택 시, 관리번호를 선택한다면 양식에 맞게 작성하도록 maskedtxtbox와 연결해주고,>
        // <                                 대출여부를 선택한다면 양식에 맞게 선택하도록 combobox와 연결해줌.>
        private void combobox_tab1_searchbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_tab1_searchbook.SelectedItem.ToString() == "관리번호")
            {
                txtbox_tab1_searchbook.Visible = false;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab1_searchbookbystatus.Visible = false;
                combobox_tab1_searchbookbystatus.SelectedItem = "";

                masktxtbox_tab1_searchbook.Visible = true;
                masktxtbox_tab1_searchbook.Mask = "000000000000";
            }
            else if (combobox_tab1_searchbook.SelectedItem.ToString() == "대출여부")
            {
                txtbox_tab1_searchbook.Visible = false;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab1_searchbookbystatus.Visible = true;
                combobox_tab1_searchbookbystatus.SelectedItem = "";

                masktxtbox_tab1_searchbook.Visible = false;
                masktxtbox_tab1_searchbook.Text = "";
            }
            else
            {
                txtbox_tab1_searchbook.Visible = true;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab1_searchbookbystatus.Visible = false;
                combobox_tab1_searchbookbystatus.SelectedItem = "";

                masktxtbox_tab1_searchbook.Visible = false;
                masktxtbox_tab1_searchbook.Text = "";
            }
        }

        // <탭 1에서, 도서검색 버튼 클릭시>
        private void btn_tab1_booksearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_tab1_searchbook.Text == "" && combobox_tab1_searchbookbystatus.SelectedItem.ToString() == "" && maskedtxtBox_tab1_searchmem.Text == "")
                {
                    MessageBox.Show("검색명 입력 후 시도 바랍니다.");
                }
                else
                {
                    if (combobox_tab1_searchbook.Text == "관리번호")
                    {
                        string insertQuery = "SELECT * FROM library_project.book WHERE 관리번호 = '" + masktxtbox_tab1_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_book = new DataTable();
                        adapter.Fill(load_data_tab1_book);
                        data_tab1_book.DataSource = load_data_tab1_book;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchbook.Text == "도서명")
                    {
                        string insertQuery = "SELECT * FROM library_project.book WHERE 이름 = '" + txtbox_tab1_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_book = new DataTable();
                        adapter.Fill(load_data_tab1_book);
                        data_tab1_book.DataSource = load_data_tab1_book;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchbook.Text == "ISBN")
                    {
                        string insertQuery = "SELECT * FROM library_project.book WHERE ISBN = '" + txtbox_tab1_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_book = new DataTable();
                        adapter.Fill(load_data_tab1_book);
                        data_tab1_book.DataSource = load_data_tab1_book;
                        connection.Close();
                    }
                    else if (combobox_tab1_searchbook.Text == "대출여부")
                    {
                        string insertQuery = "SELECT * FROM library_project.book WHERE 대출여부 = '" + combobox_tab1_searchbookbystatus.SelectedItem.ToString() + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_tab1_book = new DataTable();
                        adapter.Fill(load_data_tab1_book);
                        data_tab1_book.DataSource = load_data_tab1_book;
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {
                // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
            }
        }

        // 탭 1에서, 대출 처리 버튼 클릭 시
        private void btn_tab1_bookloan_Click(object sender, EventArgs e)
        {
            bool isMemberCanLoanStatus = true;
            bool isMemberCanStatus = false;
            bool isBookCanStatus = false;

            if (txtbox_tab1_membernum.Text == "" && txtbox_tab1_booknum.Text == "")
            {
                MessageBox.Show("회원 정보와 도서 정보를 불러온 후, 대출 대상 회원과 도서를 선택해 이용 바랍니다.");
            }
            else if (txtbox_tab1_membernum.Text == "" && txtbox_tab1_booknum.Text != "")
            {
                MessageBox.Show("대출 대상 회원을 선택 바랍니다.");
            }
            else if (txtbox_tab1_membernum.Text != "" && txtbox_tab1_booknum.Text == "")
            {
                MessageBox.Show("대출 대상 도서를 선택 바랍니다.");
            }
            else
            {
                if (txtbox_tab1_loannum.Text == "0" || txtbox_tab1_loannum.Text == "1" || txtbox_tab1_loannum.Text == "2" || txtbox_tab1_loannum.Text == "3" || txtbox_tab1_loannum.Text == "4" || txtbox_tab1_loannum.Text == "5" || txtbox_tab1_loannum.Text == "6" || txtbox_tab1_loannum.Text == "7" || txtbox_tab1_loannum.Text == "8" || txtbox_tab1_loannum.Text == "9")
                {
                    isMemberCanLoanStatus = true;
                }
                else { isMemberCanLoanStatus = false; }

                if (txtbox_tab1_memberstauts.Text != "정상") { isMemberCanStatus = false; }
                else { isMemberCanStatus = true; }

                if (txtbox_tab1_loan.Text != "대출 가능") { isBookCanStatus = false; }
                else { isBookCanStatus = true; }

                if (isMemberCanLoanStatus == true && isMemberCanStatus == true && isBookCanStatus == true)
                {
                    string insertQuery = "UPDATE library_project.member SET 대출권수 = 대출권수 + 1 WHERE 회원번호 = '" + txtbox_tab1_membernum.Text + "'; " + "UPDATE library_project.book SET 대출여부 = '대출 중' WHERE 관리번호 = '" + txtbox_tab1_booknum.Text + "'; " + "UPDATE library_project.book SET 대출한_회원번호 = '" + txtbox_tab1_membernum.Text + "' WHERE 관리번호 = '" + txtbox_tab1_booknum.Text + "';";
                    connection.Open();
                    MySqlCommand command_loan = new MySqlCommand(insertQuery, connection);
                    try
                    {
                        if (command_loan.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show(txtbox_tab1_name.Text + "회원님의 " + txtbox_tab1_bookname.Text + " 도서 대출이 완료되었습니다. 재조회 버튼을 눌러 화면을 갱신해 주세요.");

                            // 회원 정보 텍스트박스 초기화
                            txtbox_tab1_membernum.Text = "";
                            txtbox_tab1_name.Text = "";
                            txtbox_tab1_id.Text = "";
                            txtbox_tab1_loannum.Text = "";
                            txtbox_tab1_memberstauts.Text = "";
                            txtbox_tab1_gender.Text = "";
                            txtbox_tab1_membirth.Text = "";
                            txtbox_tab1_tel.Text = "";
                            txtbox_tab1_address.Text = "";

                            // 도서 정보 텍스트박스 초기화
                            txtbox_tab1_booknum.Text = "";
                            txtbox_tab1_bookname.Text = "";
                            txtbox_tab1_bookisbn.Text = "";
                            txtbox_tab1_writer.Text = "";
                            txtbox_tab1_date.Text = "";
                            txtbox_tab1_publisher.Text = "";
                            txtbox_tab1_bookstatus.Text = "";
                            txtbox_tab1_loan.Text = "";
                            txtbox_tab1_price.Text = "";
                            txtbox_tab1_pages.Text = "";
                            txtbox_tab1_bookinfo.Text = "";
                            txtbox_tab1_wholoanbook.Text = "";

                            // 데이터 조회 화면 초기화
                            data_tab1_member.DataSource = "";
                            data_tab1_book.DataSource = "";
                        }
                        else
                        {
                            MessageBox.Show("알 수 없는 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다. \n\n오류내용 : ", "도서대출 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("MySQL 연결 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다. \n\n오류내용 : " + ex.Message, "도서대출 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }
                else if (isBookCanStatus == false)
                {
                    if (txtbox_tab1_membernum.Text == txtbox_tab1_wholoanbook.Text)
                    {
                        MessageBox.Show("본 도서는 회원님께서 대출하신 도서입니다.");
                    }
                    else
                    {
                        MessageBox.Show("현재 " + txtbox_tab1_bookname.Text + "도서는 타 회원이 대출 중인 도서로, 대출이 불가능합니다.");
                    }
                }
                else if (isMemberCanLoanStatus == false && isMemberCanStatus == true && isBookCanStatus == true)
                {
                    MessageBox.Show("최대 대출 권수는 10권으로, 현재 " + txtbox_tab1_name.Text + " 회원님 께서는 " + txtbox_tab1_loannum.Text + "권을 대출하셨습니다. 대출이 불가능합니다.", "대출불가");
                }
                else if (isMemberCanLoanStatus == true && isMemberCanStatus == false && isBookCanStatus == true)
                {
                    MessageBox.Show("현재 회원 상태가 " + txtbox_tab1_memberstauts.Text + "으로, 대출이 불가능한 상태입니다.");
                }
                else
                {
                    MessageBox.Show(txtbox_tab1_name.Text + "회원님은 대출이 불가능한 회원입니다.");
                }
            }
        }

        // 탭 1에서, 반납 처리 버튼 클릭 시
        private void btn_tab1_bookback_Click(object sender, EventArgs e)
        {
            bool isBookLoanYou = false;

            if (txtbox_tab1_membernum.Text == txtbox_tab1_wholoanbook.Text)
            {
                isBookLoanYou = true;
            }
            else
            {
                isBookLoanYou = false;
            }

            if (txtbox_tab1_membernum.Text == "" && txtbox_tab1_booknum.Text == "")
            {
                MessageBox.Show("회원 정보와 도서 정보를 불러온 후, 반납 대상 회원과 도서를 선택해 이용 바랍니다.");
            }
            else if (txtbox_tab1_membernum.Text == "" && txtbox_tab1_booknum.Text != "")
            {
                MessageBox.Show("반납 대상 회원을 선택 바랍니다.");
            }
            else if (txtbox_tab1_membernum.Text != "" && txtbox_tab1_booknum.Text == "")
            {
                MessageBox.Show("반납 대상 도서를 선택 바랍니다.");
            }
            else
            {
                if (isBookLoanYou == true)
                {
                    string insertQuery = "UPDATE library_project.member SET 대출권수 = 대출권수 - 1 WHERE 회원번호 = '" + txtbox_tab1_membernum.Text + "'; " + "UPDATE library_project.book SET 대출여부 = '대출 가능' WHERE 관리번호 = '" + txtbox_tab1_booknum.Text + "'; " + "UPDATE library_project.book SET 대출한_회원번호 = '' WHERE 관리번호 = '" + txtbox_tab1_booknum.Text + "';";
                    connection.Open();
                    MySqlCommand command_bookback = new MySqlCommand(insertQuery, connection);
                    try
                    {
                        if (command_bookback.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show(txtbox_tab1_name.Text + "회원님의" + txtbox_tab1_bookname.Text + "도서 반납이 완료되었습니다. 재조회 버튼을 눌러 화면을 갱신해 주세요.");

                            // 회원 정보 텍스트박스 초기화
                            txtbox_tab1_membernum.Text = "";
                            txtbox_tab1_name.Text = "";
                            txtbox_tab1_id.Text = "";
                            txtbox_tab1_loannum.Text = "";
                            txtbox_tab1_memberstauts.Text = "";
                            txtbox_tab1_gender.Text = "";
                            txtbox_tab1_membirth.Text = "";
                            txtbox_tab1_tel.Text = "";
                            txtbox_tab1_address.Text = "";

                            // 도서 정보 텍스트박스 초기화
                            txtbox_tab1_booknum.Text = "";
                            txtbox_tab1_bookname.Text = "";
                            txtbox_tab1_bookisbn.Text = "";
                            txtbox_tab1_writer.Text = "";
                            txtbox_tab1_date.Text = "";
                            txtbox_tab1_publisher.Text = "";
                            txtbox_tab1_bookstatus.Text = "";
                            txtbox_tab1_loan.Text = "";
                            txtbox_tab1_price.Text = "";
                            txtbox_tab1_pages.Text = "";
                            txtbox_tab1_bookinfo.Text = "";
                            txtbox_tab1_wholoanbook.Text = "";

                            // 데이터 조회 화면 초기화
                            data_tab1_member.DataSource = "";
                            data_tab1_book.DataSource = "";
                        }
                        else
                        {
                            MessageBox.Show("알 수 없는 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다.", "도서대출 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("MySQL 연결 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다. \n\n오류내용 : " + ex.Message, "도서대출 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("본 도서는 회원님께서 대출하신 도서가 아닙니다. 확인 후 시도해 주세요.");
                }
            }
        }

        // <탭 2에서, 조회/재조회 버튼 클릭시>
        private void btn_tab2_loadbook_Click(object sender, EventArgs e)
        {
            
            txtbox_tab2_booknum.ReadOnly = true;
            string insertQuery_loadBook = "SELECT * FROM library_project.book;";
            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery_loadBook, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable load_data_book = new DataTable();
            adapter.Fill(load_data_book);

            data_tab2_book.DataSource = load_data_book;
            connection.Close();
        }

        // <탭 2에서, 도서목록 셀 클릭 시>
        private void data_tab2_book_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rdobtn_tab2_newmode.Checked == true)
            {
                MessageBox.Show("신규 도서 등록 모드입니다. 관리모드 변경 후 이용 바랍니다.");
            }
            else
            {
                try
                {
                    txtbox_tab2_booknum.Text = data_tab2_book.Rows[e.RowIndex].Cells[0].Value.ToString();
                    txtbox_tab2_bookname.Text = data_tab2_book.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txtbox_tab2_isbn.Text = data_tab2_book.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtbox_tab2_bookwrite.Text = data_tab2_book.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtbox_tab2_bookdate.Text = data_tab2_book.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtbox_tab2_bookpublisher.Text = data_tab2_book.Rows[e.RowIndex].Cells[5].Value.ToString();
                    combobox_tab2_bookstatus.Text = data_tab2_book.Rows[e.RowIndex].Cells[6].Value.ToString();
                    combobox_tab2_bookloanstatus.Text = data_tab2_book.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtbox_tab2_bookprice.Text = data_tab2_book.Rows[e.RowIndex].Cells[8].Value.ToString();
                    txtbox_tab2_bookpages.Text = data_tab2_book.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtbox_tab2_bookabout.Text = data_tab2_book.Rows[e.RowIndex].Cells[10].Value.ToString();
                }
                catch (Exception)
                {
                    // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
                }
            }
        }

        // <탭 2에서, 도서검색 (분류) 선택 시, 관리번호를 선택한다면 양식에 맞게 작성하도록 maskedtxtbox와 연결해주고,>
        // <                                 대출여부를 선택한다면 양식에 맞게 선택하도록 combobox와 연결해줌.>
        private void combobox_tab2_searchbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_tab2_searchbook.SelectedItem.ToString() == "관리번호")
            {
                txtbox_tab2_searchbook.Visible = false;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab2_searchbybookstatus.Visible = false;
                combobox_tab2_searchbybookstatus.SelectedItem = "";

                maskedtextbox_tab2_searchbookbynum.Visible = true;
                maskedtextbox_tab2_searchbookbynum.Mask = "000000000000";
            }
            else if (combobox_tab2_searchbook.SelectedItem.ToString() == "대출여부")
            {
                txtbox_tab2_searchbook.Visible = false;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab2_searchbybookstatus.Visible = true;
                combobox_tab2_searchbybookstatus.SelectedItem = "";

                maskedtextbox_tab2_searchbookbynum.Visible = false;
                maskedtextbox_tab2_searchbookbynum.Mask = "";
            }
            else
            {
                txtbox_tab2_searchbook.Visible = true;
                txtbox_tab1_searchbook.Text = "";

                combobox_tab2_searchbybookstatus.Visible = false;
                combobox_tab2_searchbybookstatus.SelectedItem = "";

                maskedtextbox_tab2_searchbookbynum.Visible = false;
                maskedtextbox_tab2_searchbookbynum.Mask = "";
            }
        }

        // <탭 2에서, 검색하기 버튼 클릭 시>
        private void btn_tab2_search_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbox_tab2_searchbook.Text == "" && maskedtextbox_tab2_searchbookbynum.Text == "" && combobox_tab2_searchbybookstatus.Text == "")
                {
                    MessageBox.Show("검색명 입력 후 시도 바랍니다.");
                }
                else
                {
                    if (combobox_tab2_searchbook.Text == "관리번호")
                    {
                        string insertQuery_searchBook_byNum = "SELECT * FROM library_project.book WHERE 관리번호 = '" + maskedtextbox_tab2_searchbookbynum.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byNum, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else if (combobox_tab2_searchbook.Text == "도서명")
                    {
                        string insertQuery_searchBook_byName = "SELECT * FROM library_project.book WHERE 이름 = '" + txtbox_tab2_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byName, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else if (combobox_tab2_searchbook.Text == "ISBN")
                    {
                        string insertQuery_searchBook_byISBN = "SELECT * FROM library_project.book WHERE ISBN = '" + txtbox_tab2_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byISBN, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else if (combobox_tab2_searchbook.Text == "저자")
                    {
                        string insertQuery_searchBook_byWriter = "SELECT * FROM library_project.book WHERE 저자 = '" + txtbox_tab2_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byWriter, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else if (combobox_tab2_searchbook.Text == "출판사")
                    {
                        string insertQuery_searchBook_byPublisher = "SELECT * FROM library_project.book WHERE 출판사 = '" + txtbox_tab2_searchbook.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byPublisher, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else if (combobox_tab2_searchbook.Text == "대출여부")
                    {
                        string insertQuery_searchBook_byLoan = "SELECT * FROM library_project.book WHERE 대출여부 = '" + combobox_tab2_searchbybookstatus.Text + "';";
                        connection.Open();
                        MySqlCommand command = new MySqlCommand(insertQuery_searchBook_byLoan, connection);
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        DataTable load_data_book = new DataTable();
                        adapter.Fill(load_data_book);

                        data_tab2_book.DataSource = load_data_book;
                        connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("범주 선택 후 검색 바랍니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL 연결 오류입니다. 오류보고 / 문의사항 메뉴에서 문의 바랍니다. \n\n오류내용 : " + ex.Message, "도서검색 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // <탭 2에서, 신규 버튼 클릭 시>
        private void btn_tab2_new_Click(object sender, EventArgs e)
        {
            txtbox_tab2_bookabout.Text = "";
            txtbox_tab2_isbn.Text = "";
            txtbox_tab2_bookname.Text = "";
            txtbox_tab2_bookpages.Text = "";
            txtbox_tab2_bookprice.Text = "";
            txtbox_tab2_bookpublisher.Text = "";
            txtbox_tab2_bookdate.Text = "";
            txtbox_tab2_bookwrite.Text = "";
            combobox_tab2_bookstatus.Text = "";
            combobox_tab2_bookloanstatus.Text = "";
        }

        // <탭 3에서, 조회/재조회 버튼 클릭시>
        private void btn_tab3_load_Click(object sender, EventArgs e)
        {
            string insertQuery_loadMember = "SELECT 회원번호, 이름, 아이디, 성별, 주소, 연락처, 생년월일, 이메일, 가입일, 대출권수, 회원상태 FROM library_project.member;";
            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery_loadMember, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable load_data_tab3_member = new DataTable();
            adapter.Fill(load_data_tab3_member);
            data_tab3_member.DataSource = load_data_tab3_member;
            connection.Close();
        }

        // <탭 3에서, 회원목록 셀 클릭 시>
        private void data_tab3_member_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtbox_tab3_membernum.Text = data_tab3_member.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtbox_tab3_membername.Text = data_tab3_member.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtbox_tab3_memberid.Text = data_tab3_member.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtbox_tab3_membergender.Text = data_tab3_member.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtbox_tab3_address.Text = data_tab3_member.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtbox_tab3_membertel.Text = data_tab3_member.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtbox_tab3_memberbirth.Text = data_tab3_member.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtbox_tab3_memberemail.Text = data_tab3_member.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtbox_tab3_membernewdate.Text = data_tab3_member.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtbox_tab3_loan.Text = data_tab3_member.Rows[e.RowIndex].Cells[9].Value.ToString();
                combobox_tab3_memberstatus.Text = data_tab3_member.Rows[e.RowIndex].Cells[10].Value.ToString();

            }
            catch (Exception)
            {
                // 예외처리는 따로 안함, catch문을 써야 cellclick시 에러가 발생하지 않음
            }
        }

        private void rdobtn_tab2_seemode_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_tab2_booknum.ReadOnly = true;
            txtbox_tab2_bookname.ReadOnly = true;
            txtbox_tab2_isbn.ReadOnly = true;
            txtbox_tab2_bookwrite.ReadOnly = true;
            txtbox_tab2_bookpublisher.ReadOnly = true;
            txtbox_tab2_bookdate.ReadOnly = true;
            txtbox_tab2_bookprice.ReadOnly = true;
            txtbox_tab2_bookpages.ReadOnly = true;
            txtbox_tab2_bookabout.ReadOnly = true;

            btn_tab2_save.Visible = false;
            btn_tab2_reset.Visible = false;
        }

        private void rdobtn_tab2_editmode_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_tab2_booknum.ReadOnly = true;
            txtbox_tab2_bookname.ReadOnly = false;
            txtbox_tab2_isbn.ReadOnly = false;
            txtbox_tab2_bookwrite.ReadOnly = false;
            txtbox_tab2_bookpublisher.ReadOnly = false;
            txtbox_tab2_bookdate.ReadOnly = false;
            txtbox_tab2_bookprice.ReadOnly = false;
            txtbox_tab2_bookpages.ReadOnly = false;
            txtbox_tab2_bookabout.ReadOnly = false;

            btn_tab2_save.Visible = true;
            btn_tab2_reset.Visible = true;
        }

        private void rdobtn_tab2_newmode_CheckedChanged(object sender, EventArgs e)
        {
            txtbox_tab2_booknum.ReadOnly = true;
            txtbox_tab2_bookname.ReadOnly = false;
            txtbox_tab2_isbn.ReadOnly = false;
            txtbox_tab2_bookwrite.ReadOnly = false;
            txtbox_tab2_bookpublisher.ReadOnly = false;
            txtbox_tab2_bookdate.ReadOnly = false;
            txtbox_tab2_bookprice.ReadOnly = false;
            txtbox_tab2_bookpages.ReadOnly = false;
            txtbox_tab2_bookabout.ReadOnly = false;

            btn_tab2_save.Visible = true;
            btn_tab2_reset.Visible = true;

            txtbox_tab2_booknum.Text = "";
            txtbox_tab2_bookname.Text = "";
            txtbox_tab2_isbn.Text = "";
            txtbox_tab2_bookwrite.Text = "";
            txtbox_tab2_bookpublisher.Text = "";
            txtbox_tab2_bookdate.Text = "";
            txtbox_tab2_bookprice.Text = "";
            txtbox_tab2_bookpages.Text = "";
            txtbox_tab2_bookabout.Text = "";
        }
    }
}
