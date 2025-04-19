from flask import Blueprint, request, jsonify
from services.character_service import generate_character

character_blueprint = Blueprint("character", __name__)


@character_blueprint.route("/", methods=["POST"])
def character():
    return
