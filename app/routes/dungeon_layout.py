from flask import Blueprint, request, jsonify
from app.services.dungeon_layout_service import generate_dungeon_layout

dungeon_layout_blueprint = Blueprint("dungeon_layout", __name__)


@dungeon_layout_blueprint.route("/", methods=["POST"])
def dungeon_layout():
    data = request.get_json()
    layout = data.get("layout")
    dungeon_layout = generate_dungeon_layout(layout)
    return jsonify({"dungeon_layout": dungeon_layout})
