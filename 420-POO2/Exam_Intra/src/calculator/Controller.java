package calculator;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;

import java.net.URL;
import java.util.ResourceBundle;

interface Calcul {
    double calculate(double a, double b);
}

public class Controller implements Initializable {
    @FXML
    public TextField montantInput;
    @FXML
    public Label montantPourboire;
    @FXML
    public RadioButton fifteenPercentTax;
    @FXML
    public RadioButton twentyPercentTax;
    @FXML
    public void calculerPourboire (ActionEvent event){
        try {
            Calcul multiply = (double a, double b) -> a * b;
            double pourboire = multiply.calculate(Double.parseDouble(montantInput.getText()), fifteenPercentTax.isSelected()?0.15:twentyPercentTax.isSelected()?0.2:0.00);

            montantPourboire.setText(String.format("%.2f", pourboire));
        }
        catch(Exception ex){
            Alert alert = new Alert(Alert.AlertType.ERROR, "Entrée invalide!", ButtonType.OK);
            alert.show();
        }
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
        montantPourboire.setText("0.00");
    }
}
