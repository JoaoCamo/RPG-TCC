from flask import Blueprint, request, jsonify
from app.services.image_service import generate_image

image_blueprint = Blueprint("image", __name__)

@image_blueprint.route("/", methods=["POST"])
def get_image():
    data = request.get_json()
    image_context = data.get("ImageContext")
    image_b64 = generate_image(image_context)
    return jsonify(image_b64)