from flask import Blueprint, request, jsonify
from app.services.item_service import generate_item

item_blueprint = Blueprint("item", __name__)


@item_blueprint.route("/", methods=["POST"])
def item():
    data = request.get_json()
    character = data.get("character")
    item_type = data.get("itemType")
    item = generate_item(character, item_type)
    return jsonify(item)