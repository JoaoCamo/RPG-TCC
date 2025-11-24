from flask import Blueprint, request, jsonify
from app.services.dungeon_room_service import generate_dungeon_room

dungeon_room_blueprint = Blueprint("dungeon_room", __name__)


@dungeon_room_blueprint.route("/", methods=["POST"])
def dungeon_room():
    data = request.get_json()
    selected_model = data.get("selectedModel")
    context = data.get("context")
    dungeon_room = generate_dungeon_room(selected_model ,context)
    return jsonify(dungeon_room)
