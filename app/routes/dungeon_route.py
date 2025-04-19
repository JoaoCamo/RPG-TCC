from flask import Blueprint, request, jsonify
from app.services.dungeon_service import generate_dungeon

dungeon_blueprint = Blueprint("dungeon", __name__)


@dungeon_blueprint.route("/", methods=["POST"])
def dungeon():
    return
