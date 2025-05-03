from flask import Blueprint, request, jsonify
from app.services.character_service import generate_character

character_blueprint = Blueprint("character", __name__)


@character_blueprint.route("/", methods=["POST"])
def character():
    data = request.get_json()
    level = data.get("level")
    character = generate_character(level)
    return jsonify({"character": character})
