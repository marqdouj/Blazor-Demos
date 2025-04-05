export function drawPipe(pipe) {
    const c = document.getElementById(pipe.id);
    const ctx = c.getContext("2d");
    resizeCanvasToDisplaySize(ctx);
    resetContext(ctx);
    drawEvents(ctx, pipe);
    drawLines(ctx, pipe);
    drawLabels(ctx, pipe);
}
function resizeCanvasToDisplaySize(ctx) {
    const width = ctx.canvas.clientWidth;
    const height = ctx.canvas.clientHeight;
    if (ctx.canvas.width != width || ctx.canvas.height != height) {
        ctx.canvas.width = width;
        ctx.canvas.height = height;
    }
    ctx.clearRect(0, 0, width, height);
}
function resetContext(ctx) {
    ctx.setLineDash([]);
    ctx.lineWidth = 1;
    ctx.fillStyle = "black";
    ctx.strokeStyle = "black";
    ctx.font = "16px sans-serif";
}

function drawEvents(ctx, pipe) {
    resetContext(ctx);
    pipe.events.forEach((e) => {
        if (e.isSelected) {
            setLineStyle(ctx, pipe.selectedBorder);
        }
        if (e.top.visible) {
            drawSvg(ctx, e.name, e.top);
            if (e.isSelected) {
                drawEventBorder(ctx, e.top, pipe.selectedBorder.lineWidth);
            }
        }
        if (e.bottom.visible) {
            drawSvg(ctx, e.name, e.bottom);
            if (e.isSelected) {
                drawEventBorder(ctx, e.bottom, pipe.selectedBorder.lineWidth);
            }
        }
    });
}
function drawEventBorder(ctx, rc, size) {
    ctx.beginPath();
    ctx.rect(rc.x1 - size, rc.y1 - size, rc.width + (size * 2), rc.height + (size * 2));
    ctx.stroke();
}

function drawLines(ctx, pipe) {
    resetContext(ctx);
    const cWidth = ctx.canvas.clientWidth;
    const cHeight = ctx.canvas.clientHeight;
    const centerX = cWidth / 2;
    const centerY = cHeight / 2;
    if (pipe.clockLine.visible) {
        setLineStyle(ctx, pipe.clockLine);
        ctx.beginPath();
        ctx.moveTo(centerX, 0);
        ctx.lineTo(centerX, cHeight);
        ctx.stroke();
    }
    if (pipe.rangeLine.visible) {
        setLineStyle(ctx, pipe.rangeLine);
        ctx.beginPath();
        ctx.moveTo(0, centerY);
        ctx.lineTo(cWidth, centerY);
        ctx.stroke();
    }
}
function drawLabels(ctx, pipe) {
    resetContext(ctx);
    const centerX = ctx.canvas.clientWidth / 2;
    const centerY = ctx.canvas.clientHeight / 2;
    const cHeight = ctx.canvas.clientHeight;
    const cWidth = ctx.canvas.clientWidth;
    ctx.font = pipe.labels.font;
    //OClock
    let text = pipe.clockText;
    let txtMetrics = ctx.measureText(text);
    let txtHeight = txtMetrics.fontBoundingBoxAscent + txtMetrics.fontBoundingBoxDescent;
    let txtWidth = txtMetrics.width;
    let txtTop = centerY - (txtHeight / 2);
    if (pipe.clockLine.visible) {
        const clockX = pipe.clockPosition == "Right" ? cWidth - txtWidth - 1 : 0;
        ctx.fillStyle = pipe.labels.background;
        ctx.fillRect(clockX, txtTop - 1, txtWidth + 2, txtHeight + 2);
        ctx.fillStyle = pipe.labels.foreground;
        ctx.fillText(text, clockX, txtTop + txtHeight - txtMetrics.fontBoundingBoxDescent);
    }
    //DFS Start
    text = pipe.x1;
    txtMetrics = ctx.measureText(text);
    txtWidth = txtMetrics.width;
    txtTop = cHeight - txtHeight - 1;
    if (pipe.rangeLine.visible) {
        ctx.fillStyle = pipe.labels.background;
        ctx.fillRect(0, txtTop - 1, txtWidth + 2, txtHeight + 2);
        ctx.fillStyle = pipe.labels.foreground;
        ctx.fillText(text, 0, txtTop + txtHeight - txtMetrics.fontBoundingBoxDescent);
    }
    //DFS Center
    text = pipe.xCenterUnit;
    txtMetrics = ctx.measureText(text);
    txtWidth = txtMetrics.width;
    if (pipe.rangeLine.visible) {
        ctx.fillStyle = pipe.labels.background;
        ctx.fillRect(centerX - txtWidth / 2, txtTop - 1, txtWidth + 2, txtHeight + 2);
        ctx.fillStyle = pipe.labels.foreground;
        ctx.fillText(text, centerX - txtWidth / 2, txtTop + txtHeight - txtMetrics.fontBoundingBoxDescent);
    }
    //DFS End
    text = pipe.x2;
    txtMetrics = ctx.measureText(text);
    txtWidth = txtMetrics.width;
    if (pipe.rangeLine.visible) {
        ctx.fillStyle = pipe.labels.background;
        ctx.fillRect(cWidth - txtWidth - 2, txtTop - 1, txtWidth + 2, txtHeight + 2);
        ctx.fillStyle = pipe.labels.foreground;
        ctx.fillText(text, cWidth - txtWidth - 2, txtTop + txtHeight - txtMetrics.fontBoundingBoxDescent);
    }
}
function drawSvg(ctx, image, rect) {
    let name = image + ".svg";
    let id = "svg-" + image;
    const img = document.getElementById(id);
    ctx.drawImage(img, rect.x1, rect.y1, rect.width, rect.height);
}
function setLineStyle(ctx, line) {
    ctx.lineWidth = line.lineWidth;
    ctx.strokeStyle = line.lineStroke;
    ctx.lineCap = line.lineCap;
    ctx.setLineDash(line.lineDash);
}
