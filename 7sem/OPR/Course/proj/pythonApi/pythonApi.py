import flask
import pandas as pd
import torch
import argparse
import io
from flask import request,send_file, make_response,jsonify
import cv2
from PIL import Image

import numpy as np

WEIGHT_DIR = "E:/Course/data/best_1.pt"


#model
def create_model():
    model =  torch.hub.load('yolov5', 'custom',path=r'E:/Course/data/best_1.pt', source='local') 
    return model




def get_image_from_bytes(binary_image, max_size=1024):
    input_image =Image.open(io.BytesIO(binary_image)).convert("RGB")
    width, height = input_image.size
    resize_factor = min(max_size / width, max_size / height)
    resized_image = input_image.resize((
        int(input_image.width * resize_factor),
        int(input_image.height * resize_factor)
    ))
    return resized_image


#initialize model data
CLASSES = ['background', 'cucumber']
DEVICE = torch.device('cuda') if torch.cuda.is_available() else torch.device('cpu')




detection_threshold = 0.95
app = flask.Flask(__name__)

@app.route("/health")
def health():
    return "ok"

@app.route("/v1/object-detection/yolo/image", methods=["POST", "OPTIONS"])  # type: ignore
def yolo_to_image():
    if request.method == "OPTIONS": # CORS preflight
        return _build_cors_preflight_response()
    if not request.method == "POST":
        return
    threshold = detection_threshold
    threshold_parameter = request.args.get('threshold')
    if threshold_parameter is not None:
       threshold = float(threshold_parameter)
    if request.files.get("file"):
        model.conf = threshold
        image_file = request.files["file"]
        image_bytes = image_file.read()

        image = Image.open(io.BytesIO(image_bytes))
        results = model([image],size=640)
        results.render()  # updates results.imgs with boxes and labels
        for img in results.ims:
            bytes_io = io.BytesIO()
            img_base64 = Image.fromarray(img)
            img_base64.save(bytes_io, format="png")
            return _corsify_actual_response(send_file(io.BytesIO(bytes_io.getvalue()), mimetype="image/png"))


    

@app.route("/v1/object-detection/yolo/json", methods=["POST", "OPTIONS"])  # type: ignore
def yolo_detection_to_json():
    if request.method == "OPTIONS": # CORS preflight
        return _build_cors_preflight_response()
    if not request.method == "POST":
        return
    threshold = detection_threshold
    threshold_parameter = request.args.get('threshold')
    if threshold_parameter is not None:
       threshold = float(threshold_parameter)
    if request.files.get("file"):
        image_file = request.files["file"]
        image_bytes = image_file.read()

        image = Image.open(io.BytesIO(image_bytes))
        results = model([image],size=640)
        results.render()  # updates results.imgs with boxes and labels
        return _corsify_actual_response(jsonify(results.pandas().xyxy[0].to_json(orient="records")))

def _build_cors_preflight_response():
    response = make_response()
    response.headers.add("Access-Control-Allow-Origin", "*")
    response.headers.add('Access-Control-Allow-Headers', "*")
    response.headers.add('Access-Control-Allow-Methods', "*")
    return response

def _corsify_actual_response(response):
    response.headers.add("Access-Control-Allow-Origin", "*")
    return response
parser = argparse.ArgumentParser(description="Flask api")
parser.add_argument("--port", default=5010, type=int, help="port number")
args = parser.parse_args()
model = create_model()

model = model.to(DEVICE)  # type: ignore
model.eval()
app.run(host="127.0.0.1", port=args.port,debug=True)