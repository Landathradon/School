package sample;

import javafx.application.Platform;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.control.*;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.paint.Color;

import java.net.URL;
import java.util.Optional;
import java.util.ResourceBundle;
import java.util.Timer;
import java.util.TimerTask;

public class Controller implements Initializable {

    String[] listeDeMots = {
            "Carrousel",
            "Digestion",
            "Essence",
            "Gestation",
            "Ivoire",
            "Mutation",
            "Nocturne",
            "Pagination",
            "Soleil",
            "Tentacule"
    };

    int Score = 0;
    int Errors = 0;
    String motAtrouver;
    String motCacher;
    String lettresUtiliser = "";
    Timer currentTimer;
    int timeLeft = 60;
    boolean helpUsed = false;

    @FXML
    public Label playerNameTxt;
    public Label scoreTxt;
    public Label motTxt;
    public Button startGameBtn;
    public Button aBtn;
    public Button bBtn;
    public Button cBtn;
    public Button dBtn;
    public Button eBtn;
    public Button fBtn;
    public Button gBtn;
    public Button hBtn;
    public Button iBtn;
    public Button jBtn;
    public Button kBtn;
    public Button lBtn;
    public Button mBtn;
    public Button nBtn;
    public Button oBtn;
    public Button pBtn;
    public Button qBtn;
    public Button rBtn;
    public Button sBtn;
    public Button tBtn;
    public Button uBtn;
    public Button vBtn;
    public Button wBtn;
    public Button xBtn;
    public Button yBtn;
    public Button zBtn;
    public Canvas drawingCanvas;
    public Label timerLabel;
    public Label usedLetters;
    public Button helpBtn;
    public MenuItem aboutBtn;
    public MenuItem closeBtn;
    public MenuItem instructionsBtn;
    GraphicsContext gc;

    public void startGame(ActionEvent event) {
        if (motAtrouver != null) {
            restart("");
            showScore(-1);
            return;
        }

        TextInputDialog dialog = new TextInputDialog("anonyme");
        dialog.setTitle("Débuter le jeu");
        dialog.setHeaderText("Bienvenue! Avant de commencé veuillez répondre ci-dessous");
        dialog.setContentText("Veuillez entrer votre nom:");

        Optional<String> result = dialog.showAndWait();
        result.ifPresent(name -> playerNameTxt.setText(name));

        selectRandomWordAndHide();
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        gc = drawingCanvas.getGraphicsContext2D();
        scoreTxt.setText(Score + " points");
    }


    public void selectRandomWordAndHide() {
        motAtrouver = listeDeMots[(int) Math.floor(Math.random() * listeDeMots.length)].toLowerCase();
        motCacher = motAtrouver.replaceAll("[a-zA-Z]", "*");
        motTxt.setText(motCacher);

        currentTimer = setTimer();
    }

    public void findLetters(String lettre) {
        if (motAtrouver == null) {return;}//Si le jeux n'as pas commencé..
        if (lettresUtiliser.contains(lettre)){return;}//Si la lettre à déja été utiliser..

        lettresUtiliser += lettre;
        usedLetters.setText(lettresUtiliser.replaceAll(".(?!$)", "$0 "));

        if (motAtrouver.contains(lettre)) {
            showScore(1);

            StringBuilder updated = new StringBuilder(motCacher);
            char letterToChar = lettre.charAt(0);

            int index = 0;
            for (char l : motAtrouver.toCharArray()) {
                if (l == letterToChar) {
                    updated.setCharAt(index, l);
                }
                index++;
            }

            motCacher = updated.toString();
            motTxt.setText(motCacher);
            gameOver();
        } else {
            Errors++;
            drawErrors();
        }
    }

    public void drawErrors() {
        if (Errors == 1) { //Head
            gc.setLineWidth(1);
            gc.setStroke(Color.BLACK);
            gc.setFill(Color.BEIGE);
            gc.fillRoundRect(75, 5, 50, 50, 50, 50);
            gc.strokeRoundRect(75, 5, 50, 50, 50, 50);
        } else if (Errors == 2) { //Body
            gc.setStroke(Color.DARKGREEN);
            gc.setFill(Color.BLUE);
            gc.fillRoundRect(75, 57, 50, 80, 0, 0);
            gc.strokeRoundRect(75, 57, 50, 80, 0, 0);
        } else if (Errors == 3) { //Left arm
            gc.setLineWidth(10);
            gc.setStroke(Color.BLACK);
            gc.strokeLine(55, 125, 75, 75);
        } else if (Errors == 4) { //Right arm
            gc.setStroke(Color.BLACK);
            gc.strokeLine(145, 125, 125, 75);
        } else if (Errors == 5) { //Left leg
            gc.setLineWidth(14);
            gc.setStroke(Color.BLACK);
            gc.strokeLine(82, 140, 70, 195);
        } else if (Errors == 6) { //Right leg
            gc.setStroke(Color.BLACK);
            gc.strokeLine(118, 140, 130, 195);
            gameOver();
        }
    }

    public void gameOver() {
        if (motAtrouver.equals(motCacher)) {
            showScore(5);
            restart("Félicitation !\nVotre score est de: " + Score);
        }
        if (Errors == 6 || timeLeft <= 1) {
            restart("Vous avez perdu :(\nVotre score était de: " + Score);
            showScore(-1);
        }
    }

    public void restart(String text) {
        currentTimer.cancel();
        gc.clearRect(0, 0, drawingCanvas.getWidth(), drawingCanvas.getHeight());

        Alert alert = new Alert(Alert.AlertType.CONFIRMATION);
        alert.setTitle("Game Over");
        alert.setHeaderText(text);
        alert.setContentText("Voulez-vous recommencer?");

        ButtonType buttonTypeOui = new ButtonType("Oui");
        ButtonType buttonTypeNon = new ButtonType("Non");
        ButtonType buttonTypeQuit = new ButtonType("Quitté");
        alert.getButtonTypes().setAll(buttonTypeOui, buttonTypeNon, buttonTypeQuit);

        Optional<ButtonType> result = alert.showAndWait();
        if (result.get() == buttonTypeOui) {
            selectRandomWordAndHide();
        } else if (result.get() == buttonTypeNon) {
            //
        } else if (result.get() == buttonTypeQuit) {
            System.exit(0);
        }

        helpUsed = false;
        Errors = 0;
        lettresUtiliser = "";
        usedLetters.setText(lettresUtiliser);
    }

    public void showScore(int value) {
        if (value == -1) {
            Score = 0;
        } else {
            Score += value;
        }
        scoreTxt.setText(Score + " points");
    }

    public void OnKeyPress(KeyEvent event) {
        try {
            findLetters(event.getText().toLowerCase());
        } catch (Exception ignored) {
            //Empêcher le programme de bloquer lorsque le jeux n'as pas débuté
        }
    }

    public void OnMouseClick(MouseEvent event) {
        try {
            findLetters(((Button) event.getSource()).getText().toLowerCase());
        } catch (Exception ignored) {
            //Empêcher le programme de bloquer lorsque le jeux n'as pas débuté
        }
    }

    public Timer setTimer() {
        final int[] gameLength = {60}; // Time in seconds
        Timer timer = new Timer();
        timer.scheduleAtFixedRate(new TimerTask() {
            public void run() {
                Platform.runLater(() -> {
                    timerLabel.setText("Temps restant: " + gameLength[0]);
                    if (gameLength[0] > 0) {
                        Platform.runLater(() -> timeLeft = gameLength[0]--);
                    } else {
                        gameOver();
                    }
                });
            }
        }, 0, 1000);
        return timer;
    }

    public void showInstructions(ActionEvent actionEvent) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("Instructions");
        alert.setHeaderText(null);
        String message = "Pour commencer veuillez appuyer sur le bouton 'Débuter le jeu'.\n" +
                "Ensuite vous pouvez sois utiliser les touches de votre clavier ou les boutons pour choisir les lettres.\n" +
                "\n" +
                "Il y a 3 façons de terminer le jeu.\n" +
                "- Trouver le mot\n" +
                "- Avoir 6 erreurs\n" +
                "- Avoir pris plus de 60 secondes pour trouver le mot\n" +
                "\n" +
                "Vous pouvez toujours recommencer une nouvelle session avec le bouton 'Débuter le jeux'.\n" +
                "Le bouton 'Astuce' vous servira d'aide pour trouver une lettre manquante du mot.";

        TextArea textArea = new TextArea(message);
        textArea.setEditable(false);
        textArea.setWrapText(true);
        textArea.setPrefWidth(600);
        textArea.setPrefHeight(275);

        alert.getDialogPane().setContent(textArea);
        alert.showAndWait();

    }

    public void exitGame(ActionEvent actionEvent) {
        System.exit(0);
    }

    public void showAbout(ActionEvent actionEvent) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("À propos");
        alert.setHeaderText(null);
        alert.setContentText("Bonjour, j'ai crée ce jeux dans le but de me pratiquer avec les nouvelles notions que j'ai apprises et aussi pour que vous puissiez vous amuser!");

        alert.showAndWait();
    }

    public void helpFind(ActionEvent actionEvent) {
        if(motAtrouver == null){return;}
        if(helpUsed){
            Alert alert = new Alert(Alert.AlertType.WARNING);
            alert.setTitle("Astuce");
            alert.setHeaderText(null);
            alert.setContentText("Vous avez déjà utiliser votre astuce pour ce mot.\nVeuillez réessayer dans le prochain.");

            alert.showAndWait();
            return;
        }

        helpUsed = true;
        Score--;

        String letter;
        String letterInHidden;
        do{
            int index = (int) Math.floor(Math.random() * motCacher.length());
            letterInHidden = String.valueOf(motCacher.charAt(index));
            letter = String.valueOf(motAtrouver.charAt(index));
        }while(!letterInHidden.equals("*"));

        findLetters(letter);
    }
}