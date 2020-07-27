<?php
function findDescription($arrayCours, $codeCour){
    foreach ($arrayCours as $cour) {
        if ($cour[0] === $codeCour) {
            return $cour[1];
        }
    }
    return null;
}

function getNote($arrayEval, $codeCour){
    $listEval = [];
    $result = 0;

    foreach ($arrayEval as $eval) {
        if ($eval[1] === $codeCour) {
            array_push($listEval, $eval);
        }
    }

    foreach ($listEval as $eval){
        $result += ($eval[4] * $eval[2])/100;
    }
    return $result;
}

function addToDatabase($conn, $values, $rowsEval, $postResults){
    $codeCour = "";
    if($postResults == "desc"){
        $stmt = $conn->prepare("UPDATE cours SET `desc`=? WHERE `id`=?;");
        $stmt->bind_param("ss", $values[1], $values[0]);
    } else if ($postResults == "eval"){
        $stmt = $conn->prepare("INSERT INTO evaluations (id, note) VALUES (?, ?) ON DUPLICATE KEY UPDATE note = ?;");
        $stmt->bind_param("sss", $values[0], $values[1], $values[1]);
    }

    foreach ($rowsEval as $eval){
        if($eval[0] == $values[0]){
            $codeCour = $eval[1];
            break;
        }
    }

    $stmt->execute();

    $queryEval = $conn->query("SELECT * FROM `evaluations`");
    $updRowsEval = $queryEval->fetch_all();

    $note = getNote($updRowsEval, $codeCour);
    updateResults($conn, array($codeCour, $note));
}

function updateResults($conn, $values){
    $stmt = $conn->prepare("INSERT INTO resultats (id, note) VALUES (?, ?) ON DUPLICATE KEY UPDATE note = ?;");
    $stmt->bind_param("sss", $values[0], $values[1], $values[1]);
    $stmt->execute();
}

function calculMoyenneTotal($results){
    $length = count($results);
    $moyenne = 0;
    foreach ($results as $value){
        if($value[0] == null){
            $length--;
            continue;
        }
        $moyenne += $value[1];
    }

    return ($moyenne/$length)."%";
}

function showTotalHours($arrayCour, $arrayEval){
    $listCours = [];
    $totalHours = 0;
    $totalHoursWorked = 0;

    foreach ($arrayCour as $cour){
        $totalHours += $cour[2];
        $listEval = [];
        $count = 0;
        foreach ($arrayEval as $eval) {
            if ($eval[1] === $cour[0]) {
                if($eval[4] > 0){
                    array_push($listEval, true);
                    $count++;
                } else {
                    array_push($listEval, false);
                }
            }
        }
        if(count($listEval) == $count){
            array_push($listCours, "Terminé");
            $totalHoursWorked += $cour[2];
        } elseif($count > 0){
            array_push($listCours, "En cour");
            $totalHoursWorked += (($count * $cour[2])/count($listEval));
        } else {
            array_push($listCours, "Pas commencé");
        }
    }

    $hoursSinceBeginning = getHours($totalHoursWorked);
    echo "<div class='col-md-12' style='text-align: center'>
            <h5>Date de début: 13 Avril 2020</h5>
            <p>Vous avez complété $totalHoursWorked heures de contenu.</p>
            <p>$hoursSinceBeginning</p>
            <p>Il y a $totalHours heures de contenu dans ce cour.</p>
        </div>";
}

function getHours($hoursWorked){
    $datetime1 = "2020-04-13 15:00:00";
    $timestamp1 = strtotime($datetime1);
    $timestamp2 = strtotime("now");

    $weekend = array(0, 6);

    $diff = $timestamp2 - $timestamp1;
    $one_day = 60 * 60 * 24; //number of seconds in the day

    if($diff < $one_day)
    {
        return floor($diff / 3600);
    }

    $days_between = floor($diff / $one_day);
    $remove_days  = 0;

    for($i = 1; $i <= $days_between; $i++)
    {
        $next_day = $timestamp1 + ($i * $one_day);
        if(in_array(date("w", $next_day), $weekend))
        {
            $remove_days++;
        }
    }
    $value = floor(($diff - ($remove_days * $one_day)) / 3600);
    $value2 = floor(($value/24)*20);
    $hoursTotal = $value - $value2;

    if($hoursTotal > $hoursWorked){
        return "Vous êtes en retard de: ". ($hoursTotal - $hoursWorked) . " heures ou " . (($hoursTotal - $hoursWorked)/4) . " jours";
    } elseif ($hoursTotal < $hoursWorked){
        return "Vous êtes en avance de: ". ($hoursWorked - $hoursTotal) . " heures ou " . (($hoursWorked - $hoursTotal)/4) . " jours";
    } else {

    }
}