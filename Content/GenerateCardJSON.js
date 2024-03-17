var suits = [0, 1, 2, 3];
var values = [14, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13];

var tab = "";
var output = "";
addIndent = () => {
    tab += "\t";
};
removeIndent = () => {
    tab = tab.slice (0, tab.length - 1);
};
logToOutput = (text) => {
    output += tab + text + '\n';
};

suits.forEach(suit => {
    values.forEach(value => {
        logToOutput("{");
        addIndent();
        logToOutput(`\"CardSuit\": ${suit},`);
        logToOutput(`\"CardValue\": ${value}`);
        removeIndent();
        logToOutput("},");
    })
});

console.log(output);