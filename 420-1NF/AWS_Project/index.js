const AWS = require('aws-sdk');
const dynamodb = new AWS.DynamoDB();

exports.handler = async (event, context) => {
    const tableName = 'NotesEtudiants'

    const CodeEtudiant = event['CodeEtudiant'];
    const NoteIntra = event['NoteIntra'];
    const NoteFinal = event['NoteFinal'];
    let params;

    const NoteTotal = (((NoteIntra * 40)/100) + ((NoteFinal * 60)/100));

    if (NoteTotal >= 60 ){
        params = {
            TableName: tableName,
            Item: {
                CodeEtudiant: {S: CodeEtudiant},
                ExaIntra: {N: NoteIntra.toString()},
                ExaFinal: {N: NoteFinal.toString()},
                NoteFinale: {N: NoteTotal.toString()}
            }
        };
    } else {
        params = {
            TableName: tableName,
            Item: {
                CodeEtudiant: {S: CodeEtudiant},
                ExaIntra: {N: NoteIntra.toString()},
                ExaFinal: {N: NoteFinal.toString()},
                NoteFinale: {N: NoteTotal.toString()},
                Remarque: {S: 'échec'}
            }
        };
    }

    await dynamodb.putItem(params, function(err, data) {
        if (err) {
            context.fail('ERREUR: Echec Dynamo: ' + err);
        } else {
            console.log('Dynamo OK: ' + JSON.stringify(data, null, ' '));
            context.succeed('SUCCES');
        }
    });

    return `La note finale de l'étudiant: ${CodeEtudiant} est de: ${NoteTotal}`;
}