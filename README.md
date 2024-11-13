Application de Visualisation des Journaux d'Événements
Description

Cette application Windows Forms en C# permet de visualiser, filtrer et gérer les journaux d'événements de Windows. Elle fournit une interface intuitive pour accéder aux informations d'événements système, telles que les informations, avertissements, erreurs et audits. L'application permet de charger les journaux d'événements, de visualiser leurs entrées et de filtrer les événements par type.
Fonctionnalités

    Chargement des Journaux d'Événements : L'application charge et liste les journaux d'événements du système au démarrage. Si l'accès à un journal est refusé, un message d'erreur informe l'utilisateur.

    Affichage des Événements : En sélectionnant un journal dans la liste, l'utilisateur peut voir les détails de chaque événement (niveau de sévérité, date et heure, source, catégorie et message) dans une table centralisée.

    Filtrage des Événements : Un menu déroulant permet de filtrer les événements par type (par exemple : Information, Warning, Error, SuccessAudit, FailureAudit). Cela permet à l'utilisateur de voir uniquement les événements d'intérêt.

    Suppression des Lignes Sélectionnées : L'utilisateur peut supprimer manuellement les lignes sélectionnées dans la table d'affichage, ce qui est utile pour réduire les informations visibles.

    Suppression de Tous les Événements Affichés : Un bouton permet de vider la table d'affichage de tous les événements actuellement chargés, sans affecter les données réelles du système.

    Quitter l'Application : Un bouton "Quitter" permet de fermer l'application proprement.

Structure de l'Interface Utilisateur

    Liste des Journaux d'Événements : Sur le côté gauche, une liste affiche tous les journaux d'événements accessibles. L'utilisateur peut sélectionner un journal pour charger ses événements.
    Tableau d'Événements : Une DataGridView occupe la zone centrale pour afficher les détails des événements du journal sélectionné.
    Boutons d'Action : En bas de la fenêtre, une série de boutons permet de filtrer, supprimer, vider la table ou quitter l'application.

Prérequis

    .NET Framework 4.5 ou version supérieure
    Système d'exploitation Windows (pour les journaux d'événements)

Utilisation

    Démarrer l'application : Lancez l'application, qui charge et affiche les journaux d'événements.
    Sélectionner un journal : Choisissez un journal dans la liste pour charger ses événements dans la table centrale.
    Filtrer les événements : Utilisez le menu déroulant pour afficher uniquement les événements d'un type spécifique.
    Gérer les événements affichés : Supprimez les lignes sélectionnées ou videz la table avec les boutons dédiés.
    Quitter l'application : Utilisez le bouton "Quitter" pour fermer l'application proprement.

Note

L'accès à certains journaux peut nécessiter des privilèges administratifs. En cas de permissions insuffisantes, des messages d'avertissement s'afficheront pour les journaux non accessibles.
Auteurs

Ce projet a été réalisé en C# et utilise Windows Forms pour l'interface utilisateur.
