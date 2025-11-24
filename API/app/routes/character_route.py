from flask import Blueprint, request, jsonify
from app.services.character_service import generate_character

character_blueprint = Blueprint("character", __name__)

@character_blueprint.route("/", methods=["POST"])
def character():
    data = request.get_json()
    selected_model = data.get("selectedModel")
    level = data.get("level")
    context = data.get("context")
    character = generate_character(selected_model ,level, context)
    return jsonify(character)
