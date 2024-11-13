using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Logs
{
    public partial class Form1 : Form
    {
        private List<EventLogEntry> eventEntries = new List<EventLogEntry>();

        public Form1()
        {
            // Liste pour stocker les entrées de journal d'événements temporairement
            InitializeComponent();

            // Activer les boutons
            buttonFiltrer.Enabled = true;
            buttonSuppr.Enabled = true;
            buttonToutSuppr.Enabled = true;
            buttonQuitter.Enabled = true;

            // Ajouter des options à la ComboBox de filtrage et sélectionner la première par défaut
            comboBoxFilterType.Items.AddRange(new string[] { "Information", "Warning", "Error", "SuccessAudit", "FailureAudit" });
            comboBoxFilterType.SelectedIndex = 0; // Sélection par défaut

            // Charger les journaux et configurer DataGridView
            MessageBox.Show("Chargement des journaux d'événements...");
            LoadEventLogs();
            InitializeDataGridView();
        }

        // Fonction pour charger les journaux d'événements disponibles
        private void LoadEventLogs()
        {
            try
            {
                listBox1.Items.Clear();// Effacer les items dans la liste
                foreach (var log in EventLog.GetEventLogs())
                {
                    try
                    {
                        var entryCount = log.Entries.Count; // Tester l'accès en lisant le nombre d'entrées
                        listBox1.Items.Add(log.Log); // Utiliser le nom du journal
                    }
                    catch (System.Security.SecurityException)
                    {
                        // Message si l'accès est refusé pour un journal spécifique
                        MessageBox.Show("Accès refusé pour le journal : " + log.LogDisplayName);
                    }
                    catch (Exception ex)
                    {
                        // Message pour toute autre erreur lors du chargement des journaux
                        MessageBox.Show("Erreur lors du chargement des journaux : " + ex.Message);
                    }
                }

                if (listBox1.Items.Count == 0)
                {
                    // Vérifier si aucun journal n'a été chargé
                    MessageBox.Show("Aucun journal d'événements n'a été chargé. Vérifiez les permissions.");
                }
            }
            catch (Exception ex)
            {
                // Message pour erreur critique lors du chargement
                MessageBox.Show("Erreur critique lors du chargement des journaux : " + ex.Message);
            }
        }

        // Initialisation du DataGridView avec les colonnes nécessaires
        private void InitializeDataGridView()
        {
            //initialisation de la DatagridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Niveau", "Niveau");
            dataGridView1.Columns.Add("DateEtHeure", "Date et Heure");
            dataGridView1.Columns.Add("Source", "Source");
            dataGridView1.Columns.Add("Categorie", "Catégorie");
            dataGridView1.Columns.Add("Message", "Message");

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            MessageBox.Show("DataGridView configuré avec les colonnes.");
        }
       
        // Fonction déclenchée lors de la sélection d'un journal dans la liste
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedLog = listBox1.SelectedItem.ToString(); // Récupérer le nom du journal sélectionné
                MessageBox.Show("Journal sélectionné : " + selectedLog);
                LoadEvents(selectedLog);// Charger les événements pour le journal sélectionné
            }
        }

        // Charger les événements d'un journal spécifique
        private void LoadEvents(string logName)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                InitializeDataGridView();
            }

            dataGridView1.Rows.Clear(); // Effacer les lignes existantes dans DataGridView
            eventEntries.Clear(); // Effacer la liste temporaire

            try
            {
                var log = new EventLog(logName);

                foreach (EventLogEntry entry in log.Entries)
                {
                    // Ajouter chaque entrée à la liste temporaire
                    eventEntries.Add(entry);

                    // Ajouter chaque entrée au DataGridView pour affichage initial
                    dataGridView1.Rows.Add(entry.EntryType, entry.TimeGenerated, entry.Source, entry.Category, entry.Message);
                }

                MessageBox.Show("Événements chargés pour le journal : " + logName);
            }
            catch (Exception ex)
            {
                // Message en cas d'erreur lors du chargement des événements
                MessageBox.Show("Erreur lors du chargement des événements du journal '" + logName + "' : " + ex.Message);
            }
        }

        // Fonction pour supprimer les lignes sélectionnées dans le DataGridView
        private void buttonSuppr_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        // Fonction pour supprimer toutes les lignes du DataGridView
        private void buttonToutSuppr_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
       
        // Fonction pour quitter l'application
        private void buttonQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Fonction pour filtrer les événements selon le type sélectionné dans la ComboBox
        private void buttonFiltrer_Click(object sender, EventArgs e)
        {
            // Vérification si comboBoxFilterType ou SelectedItem est nul
            if (comboBoxFilterType == null || comboBoxFilterType.SelectedItem == null)
            {
                MessageBox.Show("Erreur : Veuillez sélectionner un niveau de filtre dans la ComboBox.");
                return;
            }

            // Récupérer le niveau de filtrage sélectionné dans la ComboBox
            string filterType = comboBoxFilterType.SelectedItem.ToString();

            // Effacer les lignes actuelles dans le DataGridView sans toucher à la liste d'origine
            dataGridView1.Rows.Clear();

            // Parcourir la liste temporaire et afficher uniquement les éléments correspondants
            foreach (var entry in eventEntries)
            {
                if (entry.EntryType.ToString() == filterType)
                {
                    dataGridView1.Rows.Add(entry.EntryType, entry.TimeGenerated, entry.Source, entry.Category, entry.Message);
                }
            }

            MessageBox.Show("Filtrage terminé pour le niveau : " + filterType);
        }

    }
}
