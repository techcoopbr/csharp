namespace TechCloud
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.btn_listar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.bt_inserir = new System.Windows.Forms.Button();
            this.bt_list_unico = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LISTAR = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_nome = new System.Windows.Forms.Label();
            this.txt_listar = new System.Windows.Forms.TextBox();
            this.bt_conectar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.LISTAR.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_listar
            // 
            this.btn_listar.Location = new System.Drawing.Point(84, 162);
            this.btn_listar.Name = "btn_listar";
            this.btn_listar.Size = new System.Drawing.Size(75, 23);
            this.btn_listar.TabIndex = 0;
            this.btn_listar.Text = "LISTAR";
            this.btn_listar.UseVisualStyleBackColor = true;
            this.btn_listar.Click += new System.EventHandler(this.btn_listar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(253, 137);
            this.dataGridView1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(148, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // bt_inserir
            // 
            this.bt_inserir.Location = new System.Drawing.Point(94, 58);
            this.bt_inserir.Name = "bt_inserir";
            this.bt_inserir.Size = new System.Drawing.Size(75, 23);
            this.bt_inserir.TabIndex = 4;
            this.bt_inserir.Text = "INSERIR";
            this.bt_inserir.UseVisualStyleBackColor = true;
            this.bt_inserir.Click += new System.EventHandler(this.bt_inserir_Click);
            // 
            // bt_list_unico
            // 
            this.bt_list_unico.Location = new System.Drawing.Point(165, 44);
            this.bt_list_unico.Name = "bt_list_unico";
            this.bt_list_unico.Size = new System.Drawing.Size(75, 23);
            this.bt_list_unico.TabIndex = 5;
            this.bt_list_unico.Text = "LISTAR";
            this.bt_list_unico.UseVisualStyleBackColor = true;
            this.bt_list_unico.Click += new System.EventHandler(this.bt_list_unico_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status.Location = new System.Drawing.Point(231, 7);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(147, 18);
            this.lbl_status.TabIndex = 6;
            this.lbl_status.Text = "DESCONECTADO";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.bt_inserir);
            this.groupBox1.Location = new System.Drawing.Point(292, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 98);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INSERIR";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "NOME";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "NUMERO";
            // 
            // LISTAR
            // 
            this.LISTAR.Controls.Add(this.dataGridView1);
            this.LISTAR.Controls.Add(this.btn_listar);
            this.LISTAR.Location = new System.Drawing.Point(4, 32);
            this.LISTAR.Name = "LISTAR";
            this.LISTAR.Size = new System.Drawing.Size(265, 191);
            this.LISTAR.TabIndex = 8;
            this.LISTAR.TabStop = false;
            this.LISTAR.Text = "LISTAR";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lbl_nome);
            this.groupBox2.Controls.Add(this.txt_listar);
            this.groupBox2.Controls.Add(this.bt_list_unico);
            this.groupBox2.Location = new System.Drawing.Point(292, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 87);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PESQUISA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "NUMERO";
            // 
            // lbl_nome
            // 
            this.lbl_nome.AutoSize = true;
            this.lbl_nome.Location = new System.Drawing.Point(120, 49);
            this.lbl_nome.Name = "lbl_nome";
            this.lbl_nome.Size = new System.Drawing.Size(39, 13);
            this.lbl_nome.TabIndex = 7;
            this.lbl_nome.Text = "NOME";
            // 
            // txt_listar
            // 
            this.txt_listar.Location = new System.Drawing.Point(6, 46);
            this.txt_listar.Name = "txt_listar";
            this.txt_listar.Size = new System.Drawing.Size(100, 20);
            this.txt_listar.TabIndex = 6;
            // 
            // bt_conectar
            // 
            this.bt_conectar.Location = new System.Drawing.Point(150, 6);
            this.bt_conectar.Name = "bt_conectar";
            this.bt_conectar.Size = new System.Drawing.Size(75, 23);
            this.bt_conectar.TabIndex = 10;
            this.bt_conectar.Text = "CONECTAR";
            this.bt_conectar.UseVisualStyleBackColor = true;
            this.bt_conectar.Click += new System.EventHandler(this.bt_conectar_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 229);
            this.Controls.Add(this.bt_conectar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.LISTAR);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_status);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TechCloud Comunicador";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.LISTAR.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_listar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button bt_inserir;
        private System.Windows.Forms.Button bt_list_unico;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox LISTAR;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_nome;
        private System.Windows.Forms.TextBox txt_listar;
        private System.Windows.Forms.Button bt_conectar;
    }
}

