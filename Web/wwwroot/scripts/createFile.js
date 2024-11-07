function getFullText() {
    const fullText = document.body.innerText; // Или используйте другой элемент, если нужно
    return fullText;
}

function downloadTextAsFile() {
    const text = getFullText();
    const blob = new Blob([text], { type: 'text/plain' });
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = 'report.txt'; // Имя файла
    document.body.appendChild(a);
    a.click();
    a.remove();
    window.URL.revokeObjectURL(url); // Освобождаем URL
}